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
                TempData["ObjectData"] = JsonConvert.SerializeObject(new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
                return RedirectToAction("Error", "Home");
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
                    TempData["ObjectData"] = JsonConvert.SerializeObject(new ErrorViewModel { Message = "El cliente no existe", ErrorCode = 404 });
                    return RedirectToAction("Error", "Home");
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
                TempData["ObjectData"] = JsonConvert.SerializeObject(new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
                return RedirectToAction("Error", "Home");
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
                    if(_workContainer.Client.GetOne(newSale.ClientID) == null) return customBadRequest("El cliente ingresado no existe");
                    if(_workContainer.GlassType.GetOne(newSale.GlassTypeID) == null) return customBadRequest("El tipo de vidrio ingresado no existe");
                    if(_workContainer.GlassColor.GetOne(newSale.GlassColorID) == null) return customBadRequest("El color de vidrio ingresado no existe");
                    if(_workContainer.Doctor.GetOne(newSale.DoctorID) == null) return customBadRequest("El médico ingresado no existe");
                    if(_workContainer.Seller.GetOne(newSale.SellerID) == null) return customBadRequest("El vendedor/a ingresado/a no existe");
                    //if (newSale.FrameID != 0) // Agregar este if si pongo que el marco puede ser nulo al crear la venta
                    if(_workContainer.Frame.GetOne(newSale.FrameID) == null) return customBadRequest("El marco ingresado no existe");

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
                    return customBadRequest("Intente nuevamente o comuníquese para soporte", e.Message);
                }
            }
            return customBadRequest("Alguno de los campos ingresados no es válido");
        }

        private IActionResult customBadRequest(string message, string error = null)
        {
            return BadRequest(new
            {
                success = false,
                title = "Error al crear la venta",
                message,
                error,
            });
        }

        /*[HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndexViewModel provider)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Provider.IsDuplicated(provider.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al editar el proveedor",
                            message = "Ya existe otro con el mismo nombre y apellido",
                        });
                    }
                    _workContainer.Provider.Update(provider.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = provider.CreateViewModel,
                        message = "El proveedor se editó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar el proveedor",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar el proveedor",
                message = "Alguno de los campos ingresados no es válido",
            });
        }*/

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