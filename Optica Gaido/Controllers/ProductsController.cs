using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Products;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public ProductsController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IndexViewModel viewModel = new()
                {
                    Products = _workContainer.Product.GetAll(),
                    CreateViewModel = new Product()
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
        public IActionResult Create(IndexViewModel prodService)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    prodService.CreateViewModel.CreatedAt = DateTime.UtcNow.AddHours(-3);
                    _workContainer.Product.Add(prodService.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = prodService.CreateViewModel,
                        message = "El " + (prodService.CreateViewModel.Stock != null ? "producto" : "servicio") + " se creó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al crear el " + (prodService.CreateViewModel.Stock != null ? "producto" : "servicio"),
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al crear el " + (prodService.CreateViewModel.Stock != null ? "producto" : "servicio"),
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndexViewModel prodService)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _workContainer.Product.Update(prodService.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = prodService.CreateViewModel,
                        message = "El " + (prodService.CreateViewModel.Stock != null ? "producto" : "servicio") + " se editó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar el " + (prodService.CreateViewModel.Stock != null ? "producto" : "servicio"),
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar el " + (prodService.CreateViewModel.Stock != null ? "producto" : "servicio"),
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(long id)
        {
            try
            {
                Product prodService = _workContainer.Product.GetOne(id);
                if (prodService != null)
                {
                    _workContainer.Product.SoftDelete(id);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = id,
                        message = "El " + (prodService.Stock != null ? "producto" : "servicio") + " se eliminó correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al eliminar",
                    message = "No se encontró el " + (prodService.Stock != null ? "producto" : "servicio") + " solicitado",
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

        #endregion
    }
}