using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        private IActionResult CustomBadRequest(string message, string error = null)
        {
            return BadRequest(new
            {
                success = false,
                title = "Error al crear la venta",
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
                    Sales = _workContainer.Sale.GetAll(filter, includeProperties: "Client, SalePaymentMethods, SalePaymentMethods.PaymentMethod"),
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
                foreach (var method in _workContainer.PaymentMethod.GetAll())
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
                Sale sale = _workContainer.Sale.GetFirstOrDefault(filter, includeProperties: "Client, Client.HealthInsurance, SalePaymentMethods, SalePaymentMethods.PaymentMethod");
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
                    GlassColors = _workContainer.GlassColor.GetDropDownList(),
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
                _workContainer.BeginTransaction();
                try
                {
                    Sale newSale = sale.CreateViewModel;
                    if(_workContainer.Client.GetOne(newSale.ClientID) == null) return CustomBadRequest("El cliente ingresado no existe");
                    if(_workContainer.GlassType.GetOne(newSale.GlassTypeID) == null) return CustomBadRequest("El tipo de vidrio ingresado no existe");
                    if(_workContainer.GlassColor.GetOne(newSale.GlassColorID) == null) return CustomBadRequest("El color de vidrio ingresado no existe");
                    if(_workContainer.Doctor.GetOne(newSale.DoctorID) == null) return CustomBadRequest("El médico ingresado no existe");
                    if(_workContainer.Seller.GetOne(newSale.SellerID) == null) return CustomBadRequest("El vendedor/a ingresado/a no existe");
                    //if (newSale.FrameID != 0) // Agregar este if si pongo que el marco puede ser nulo al crear la venta
                    if(_workContainer.Frame.GetOne(newSale.FrameID) == null) return CustomBadRequest("El marco ingresado no existe");

                    newSale.CreatedAt = DateTime.UtcNow.AddHours(-3);
                    _workContainer.Sale.Add(newSale);
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
                catch (Exception e)
                {
                    _workContainer.Rollback();
                    return CustomBadRequest("Intente nuevamente o comuníquese para soporte", e.Message);
                }
            }
            return CustomBadRequest("Alguno de los campos ingresados no es válido");
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DetailsViewModel sale)
        {
            Sale newSale = sale.Sale;
            if (ModelState.IsValid)
            {
                try
                {
                    
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar la venta",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar la venta",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(long id)
        {
            try
            {
                var provider = _workContainer.Provider.GetOne(id);
                if (provider != null)
                {
                    _workContainer.Provider.SoftDelete(id);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = id,
                        message = "El proveedor se eliminó correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al eliminar",
                    message = "No se encontró el proveedor solicitado",
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    success = false,
                    title = "Error al eliminar",
                    message = "Intente nuevamente o comuníquese para soporte",
                    error = e.Message,
                });
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
                return BadRequest(new
                {
                    success = false,
                    title = "Error al recuperar las ventas del " + year,
                    message = "Intente nuevamente o comuníquese para soporte",
                    error = e.Message,
                });
            }
        }
        #endregion
    }
}