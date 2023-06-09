using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Optica_Gaido.Data;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Brands;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class BrandsController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public BrandsController(IWorkContainer workContainer)
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
                    Brands = _workContainer.Brand.GetAll(),
                    CreateViewModel = new Brand()
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
        public IActionResult Create(IndexViewModel brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Brand.IsDuplicated(brand.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al agregar la marca",
                            message = "Ya existe otra con el mismo nombre",
                        });
                    }
                    _workContainer.Brand.Add(brand.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = brand.CreateViewModel,
                        message = "La marca se agreg� correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al agregar la marca",
                        message = "Intente nuevamente o comun�quese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al agregar la marca",
                message = "Alguno de los campos ingresados no es v�lido",
            });
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndexViewModel brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Brand.IsDuplicated(brand.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al editar la marca",
                            message = "Ya existe otra con el mismo nombre",
                        });
                    }
                    _workContainer.Brand.Update(brand.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = brand.CreateViewModel,
                        message = "La marca se edit� correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar la marca",
                        message = "Intente nuevamente o comun�quese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar la marca",
                message = "Alguno de los campos ingresados no es v�lido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeState(long id)
        {
            try
            {
                var brand = _workContainer.Brand.GetOne(id);
                if (brand != null)
                {
                    _workContainer.Brand.ChangeState(id);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = brand,
                        message = "La marca se di� de " + (brand.IsActive ? "alta" : "baja") + " correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al cambiar el estado",
                    message = "No se encontr� la marca solicitada",
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    success = false,
                    title = "Error al cambiar el estado",
                    message = "Intente nuevamente o comun�quese para soporte",
                    error = e.Message,
                });
            }
        }

        #region Llamadas a la API

        #endregion
    }
}