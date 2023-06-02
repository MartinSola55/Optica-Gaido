using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

            List<GlassFormat> formats = new()
            {
                new GlassFormat()
                {
                    Eye = Eye.Derecho,
                    Distance = Distance.Lejos
                },
                new GlassFormat()
                {
                    Eye = Eye.Izquierdo,
                    Distance = Distance.Lejos
                },
                new GlassFormat()
                {
                    Eye = Eye.Derecho,
                    Distance = Distance.Cerca
                },
                new GlassFormat()
                {
                    Eye = Eye.Izquierdo,
                    Distance = Distance.Cerca
                }
            };

            NewViewModel viewModel = new()
            {
                Doctors = _workContainer.Doctor.GetDropDownList(),
                Sellers = _workContainer.Seller.GetDropDownList(),
                SalePaymentMethods = methods,
                GlassFormats = formats,
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
            if (ModelState.IsValid)
            {
                try
                {
                    //if (_workContainer.Provider.IsDuplicated(provider.CreateViewModel))
                    //{
                    //    return BadRequest(new
                    //    {
                    //        success = false,
                    //        title = "Error al agregar el proveedor",
                    //        message = "Ya existe otro con el mismo nombre y apellido",
                    //    });
                    //}
                    //_workContainer.Provider.Add(provider.CreateViewModel);
                    //_workContainer.Save();
                    //return Json(new
                    //{
                    //    success = true,
                    //    data = provider.CreateViewModel,
                    //    message = "El provider se agregó correctamente",
                    //});
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al crear la venta",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al crear la venta",
                message = "Alguno de los campos ingresados no es válido",
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