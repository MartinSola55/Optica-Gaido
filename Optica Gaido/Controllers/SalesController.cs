using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Optica_Gaido.Data;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Sales;

namespace Optica_Gaido.Controllers
{
    public class SalesController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public SalesController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult New(long id)
        {
            Client client = _workContainer.Client.GetOneWithProperties(id, properties: "HealthInsurance");
            if (client == null)
            {
                return this.CustomError(new ErrorViewModel { Message = "El cliente no existe", ErrorCode = 404 });
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
                SalePaymentMethods = methods,
                GlassTypes = _workContainer.GlassType.GetAll(),
                GlassColors = _workContainer.GlassColor.GetDropDownList(),
                Client = client
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CustomError(ErrorViewModel error)
        {
            return View("~/Views/Error/CustomError.cshtml", error);
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
                    if(_workContainer.Seller.GetOne(newSale.SellerID) == null) return customBadRequest("El vendedor ingresado no existe");

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
                        data = sale.CreateViewModel,
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
    }
}