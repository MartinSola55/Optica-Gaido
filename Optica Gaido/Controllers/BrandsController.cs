using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Optica_Gaido.Data;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Brands;

namespace Optica_Gaido.Controllers
{
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
            IndexViewModel viewModel = new()
            {
                Brands = _workContainer.Brand.GetAll(),
                CreateViewModel = new Brand()
            };
            return View(viewModel);
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
                        message = "La marca se agregó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al agregar la marca",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al agregar la marca",
                message = "Alguno de los campos ingresados no es válido",
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
                        message = "La marca se editó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar la marca",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar la marca",
                message = "Alguno de los campos ingresados no es válido",
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
                    string state = brand.IsActive ? "alta" : "baja";
                    return Json(new
                    {
                        success = true,
                        data = brand,
                        message = "La marca se dió de " + state + " correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al cambiar el estado",
                    message = "No se encontró la marca solicitada",
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    success = false,
                    title = "Error al cambiar el estado",
                    message = "Intente nuevamente o comuníquese para soporte",
                    error = e.Message,
                });
            }
        }

        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetDropDownList()
        {
            return Json(new { data = _workContainer.Brand.GetDropDownList() });
        }
        #endregion
    }
}