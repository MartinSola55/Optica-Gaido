using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using Optica_Gaido.Data;
using Optica_Gaido.Data.Repository;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Sales;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public SalesController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        private IActionResult CustomBadRequest(string title, string message, string error = null)
        {
            return BadRequest(new
            {
                success = false,
                title,
                message,
                error,
            });
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                Expression<Func<Sale, bool>> filter = sale => sale.CreatedAt.Year == DateTime.UtcNow.AddHours(-3).Year;
                IndexViewModel viewModel = new()
                {
                    Sales = _workContainer.Sale.GetAll(filter, includeProperties: "Client, SalePaymentMethods, SalePaymentMethods.PaymentMethod").OrderByDescending(x => x.CreatedAt),
                    Years = _workContainer.Sale.GetYears()
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpGet]
        public IActionResult New(long id)
        {
            try
            {
                Client client = _workContainer.Client.GetOneWithProperties(id, properties: "HealthInsurance");
                if (client == null)
                {
                    return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "El cliente no existe", ErrorCode = 404 });
                }

                List<SalePaymentMethod> methods = new();
                foreach (var method in _workContainer.PaymentMethod.GetAll(hasIsActive: true))
                {
                    methods.Add(new SalePaymentMethod() { PaymentMethod = method });
                }

                NewViewModel viewModel = new()
                {
                    Doctors = _workContainer.Doctor.GetDropDownList(),
                    Sellers = _workContainer.Seller.GetDropDownList(),
                    Frames = _workContainer.Frame.GetAll(includeProperties: "Brand, Material"),
                    SalePaymentMethods = methods,
                    GlassTypes = _workContainer.GlassType.GetAll(),
                    GlassFocusTypes = _workContainer.GlassFocusType.GetDropDownList(),
                    GlassColors = _workContainer.GlassColor.GetDropDownList(),
                    Client = client
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpGet]
        public IActionResult Details(long id)
        {
            try
            {
                Expression<Func<Sale, bool>> filter = sale => sale.ID == id;
                Sale sale = _workContainer.Sale.GetFirstOrDefault(filter, includeProperties: "Client, Client.HealthInsurance, SalePaymentMethods, SalePaymentMethods.PaymentMethod, GlassFormats");
                if (sale == null)
                {
                    return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "La venta no existe", ErrorCode = 404 });
                }

                DetailsViewModel viewModel = new()
                {
                    Sale = sale,
                    Doctors = _workContainer.Doctor.GetDropDownList(),
                    Sellers = _workContainer.Seller.GetDropDownList(),
                    GlassTypes = _workContainer.GlassType.GetAll(),
                    GlassFocusTypes = _workContainer.GlassFocusType.GetDropDownList(),
                    GlassColors = _workContainer.GlassColor.GetDropDownList(),
                    PaymentMethods = _workContainer.PaymentMethod.GetAll(hasIsActive: true)
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewViewModel sale)
        {
            ModelState.Remove("Doctors");
            ModelState.Remove("Sellers");
            ModelState.Remove("SalePaymentMethods");
            ModelState.Remove("GlassTypes");
            ModelState.Remove("GlassColors");
            ModelState.Remove("Client");
            ModelState.Remove("Client.Name");
            ModelState.Remove("Client.Surname");
            ModelState.Remove("Client.Phone");
            ModelState.Remove("Client.Adress");
            if (ModelState.IsValid)
            {
                try
                {
                    _workContainer.BeginTransaction();

                    Sale newSale = sale.CreateViewModel;
                    if(_workContainer.Client.GetOne(newSale.ClientID) == null) return CustomBadRequest(title: "Error al crear la venta", message: "El cliente ingresado no existe");
                    if(_workContainer.GlassType.GetOne(newSale.GlassTypeID) == null) return CustomBadRequest(title: "Error al crear la venta", message: "El tipo de vidrio ingresado no existe");
                    if(_workContainer.GlassColor.GetOne(newSale.GlassColorID) == null) return CustomBadRequest(title: "Error al crear la venta", message: "El color de vidrio ingresado no existe");
                    if(_workContainer.Doctor.GetOne(newSale.DoctorID) == null) return CustomBadRequest(title: "Error al crear la venta", message: "El médico ingresado no existe");
                    if(_workContainer.Seller.GetOne(newSale.SellerID) == null) return CustomBadRequest(title: "Error al crear la venta", message: "El vendedor/a ingresado/a no existe");
                    //if (newSale.FrameID != 0) // Agregar este if si pongo que el marco puede ser nulo al crear la venta
                    if(_workContainer.Frame.GetOne(newSale.FrameID) == null) return CustomBadRequest(title: "Error al crear la venta", message: "El marco ingresado no existe");

                    newSale.CreatedAt = DateTime.UtcNow.AddHours(-3);

                    newSale.DeliveryDate = DateTime.ParseExact(sale.CreateViewModel.DeliveryDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    _workContainer.Sale.Add(newSale);

                    Client client = _workContainer.Client.GetOne(newSale.ClientID);

                    // Asignar deuda al cliente
                    client.Debt += newSale.Price;
                    foreach (SalePaymentMethod item in newSale.SalePaymentMethods)
                    {
                        client.Debt -= item.Amount;
                        item.CreatedAt = DateTime.UtcNow.AddHours(-3);
                    }

                    _workContainer.Save();

                    foreach (var format in newSale.GlassFormats)
                    {
                        format.SaleID = newSale.ID;
                    }

                    foreach (var method in newSale.SalePaymentMethods)
                    {
                        method.SaleID = newSale.ID;
                    }

                    _workContainer.Commit();
                    return Json(new
                    {
                        success = true,
                        message = "La venta se creó correctamente",
                    });
                }
                catch (FormatException)
                {
                    _workContainer.Rollback();
                    return CustomBadRequest(title: "Error al editar la venta", message: "Alguno de los campos ingresados no posee un formato válido");
                }
                catch (Exception e)
                {
                    _workContainer.Rollback();
                    return CustomBadRequest("Intente nuevamente o comuníquese para soporte", e.Message);
                }
            }
            return CustomBadRequest(title: "Error al crear la venta", message: "Alguno de los campos ingresados no es válido");
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Sale")] DetailsViewModel editedSale)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _workContainer.BeginTransaction();

                    Sale newSale = editedSale.Sale;
                    if (_workContainer.GlassType.GetOne(newSale.GlassTypeID) == null) return CustomBadRequest(title: "Error al editar la venta", message: "El tipo de vidrio ingresado no existe");
                    if (_workContainer.GlassColor.GetOne(newSale.GlassColorID) == null) return CustomBadRequest(title: "Error al editar la venta", message: "El color de vidrio ingresado no existe");
                    if (_workContainer.Doctor.GetOne(newSale.DoctorID) == null) return CustomBadRequest(title: "Error al editar la venta", message: "El médico ingresado no existe");
                    if (_workContainer.Seller.GetOne(newSale.SellerID) == null) return CustomBadRequest(title: "Error al editar la venta", message: "El vendedor/a ingresado/a no existe");
                    //if (newSale.FrameID != 0) // Agregar este if si pongo que el marco puede ser nulo al editar la venta
                    //if (_workContainer.Frame.GetOne(newSale.FrameID) == null) return CustomBadRequest(title: "Error al editar la venta", message: "El marco ingresado no existe");
                    newSale.DeliveryDate = DateTime.ParseExact(editedSale.Sale.DeliveryDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    // Recalcular deuda del cliente
                    Sale oldSale = _workContainer.Sale.GetOne(editedSale.Sale.ID);
                    Client client = _workContainer.Client.GetOne(oldSale.ClientID);

                    client.Debt -= oldSale.Price;

                    client.Debt += newSale.Price;

                    _workContainer.Sale.Update(newSale);

                    _workContainer.Commit();
                    return Json(new
                    {
                        success = true,
                        message = "La venta se editó correctamente",
                    });
                }
                catch (FormatException)
                {
                    _workContainer.Rollback();
                    return CustomBadRequest(title: "Error al editar la venta", message: "Alguno de los campos ingresados no posee un formato válido");
                }
                catch (Exception e)
                {
                    _workContainer.Rollback();
                    return CustomBadRequest(title: "Error al editar la venta", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
                }
            }
            return CustomBadRequest(title: "Error al editar la venta", message: "Alguno de los campos ingresados no es válido");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateFormats([Bind("GlassFormats, ID")] Sale editedFormats)
        {
            ModelState.Remove("Dip");
            ModelState.Remove("DeliveryDateString");
            if (ModelState.IsValid)
            {
                try
                {
                    _workContainer.BeginTransaction();

                    Expression<Func<Sale, bool>> filter = sale => sale.ID == editedFormats.ID;
                    Sale sale = _workContainer.Sale.GetFirstOrDefault(filter, includeProperties: "GlassFormats");

                    if (sale == null) return CustomBadRequest(title: "Error al editar los formatos", message: "No se encontró la venta solicitada");

                    foreach (var format in sale.GlassFormats)
                    {
                        _workContainer.GlassFormat.Remove(format);
                    }

                    foreach (var format in editedFormats.GlassFormats)
                    {
                        sale.GlassFormats.Add(format);
                    }
                    _workContainer.Save();

                    _workContainer.Commit();
                    return Json(new
                    {
                        success = true,
                        message = "Los formatos se editaron correctamente",
                    });
                }
                catch (Exception e)
                {
                    _workContainer.Rollback();
                    return CustomBadRequest(title: "Error al editar los formatos", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
                }
            }
            return CustomBadRequest(title: "Error al editar los formatos", message: "Alguno de los campos ingresados no es válido");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePaymentMethods(Sale request)
        {
            ModelState.Remove("Dip");
            ModelState.Remove("DeliveryDateString");
            if (ModelState.IsValid)
            {
                try
                {
                    _workContainer.BeginTransaction();

                    Expression<Func<Sale, bool>> filter = sale => sale.ID == request.ID;
                    Sale sale = _workContainer.Sale.GetFirstOrDefault(filter, includeProperties: "SalePaymentMethods, Client");

                    if (sale == null) return CustomBadRequest(title: "Error al editar los métodos de pago", message: "No se encontró la venta solicitada");

                    List<SalePaymentMethod> paymentMethods = sale.SalePaymentMethods.ToList();
                    foreach (var pm in paymentMethods)
                    {
                        bool removePM = !request.SalePaymentMethods.Any(x => x.PaymentMethodID == pm.PaymentMethodID);
                        sale.Client.Debt += pm.Amount;
                        if (removePM) _workContainer.SalePaymentMethod.Remove(pm);
                    }
                    foreach (var pm in request.SalePaymentMethods)
                    {
                        SalePaymentMethod new_pm = new()
                        {
                            PaymentMethodID = pm.PaymentMethodID,
                            SaleID = sale.ID,
                            CreatedAt = DateTime.UtcNow.AddHours(-3),
                        };

                        // Si existe se asigna la diferencia, sino el total
                        if (paymentMethods.Any(x => x.PaymentMethodID == pm.PaymentMethodID))
                        {
                            new_pm.Amount = pm.Amount - paymentMethods.Where(x => x.PaymentMethodID == pm.PaymentMethodID).Sum(x => x.Amount);
                        } else
                        {
                            new_pm.Amount = pm.Amount;
                        }
                        if (new_pm.Amount != 0) _workContainer.SalePaymentMethod.Add(new_pm);
                        sale.Client.Debt -= pm.Amount;
                    }

                    _workContainer.Save();
                    _workContainer.Commit();
                    return Json(new
                    {
                        success = true,
                        message = "Los métodos de pago se editaron correctamente",
                    });
                }
                catch (Exception e)
                    {
                    _workContainer.Rollback();
                    return CustomBadRequest(title: "Error al editar los métodos de pago", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
                }
            }
            return CustomBadRequest(title: "Error al editar los métodos de pago", message: "Alguno de los campos ingresados no es válido");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(long id)
        {
            try
            {
                var sale = _workContainer.Sale.GetOne(id);
                if (sale != null)
                {
                    _workContainer.Sale.SoftDelete(id);

                    Client client = _workContainer.Client.GetOne(sale.ClientID);
                    client.Debt -= sale.Price;
                    if (sale.Deposit != null) client.Debt += sale.Deposit.Value;

                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = id,
                        message = "La venta se eliminó correctamente",
                    });
                }
                return CustomBadRequest(title: "Error al eliminar", message: "No se encontró la venta solicitada");
            }
            catch (SqlException e)
            {
                return CustomBadRequest(title: "Error en la base de datos", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al eliminar la venta", message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }


        #region Llamadas a la API

        [HttpGet]
        public IActionResult GetSalesByYear(string year)
        {
            try
            {
                Expression<Func<Sale, bool>> filter = sale => sale.CreatedAt.Year.ToString() == year;
                var sales = _workContainer.Sale.GetAll(filter, includeProperties: "Client, SalePaymentMethods, SalePaymentMethods.PaymentMethod")
                    .Select(x => new
                    {
                        id = x.ID,
                        name = x.Client.Name,
                        surname = x.Client.Surname,
                        createdAt = x.CreatedAt,
                        deliveryDate = x.DeliveryDate,
                        price = x.Price,
                        deposit = x.Deposit,
                        paymentMethods = x.SalePaymentMethods
                    });
                return Json(new
                {
                    success = true,
                    data = sales
                });
            }
            catch (Exception e)
            {
                return CustomBadRequest(title: "Error al recuperar las ventas del " + year, message: "Intente nuevamente o comuníquese para soporte", error: e.Message);
            }
        }
        #endregion
    }
}