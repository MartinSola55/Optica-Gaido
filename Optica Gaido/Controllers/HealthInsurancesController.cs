using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Optica_Gaido.Data;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.HealthInsurances;

namespace Optica_Gaido.Controllers
{
    public class HealthInsurancesController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public HealthInsurancesController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IndexViewModel viewModel = new()
            {
                HealthInsurances = _workContainer.HealthInsurance.GetAll(),
                CreateViewModel = new HealthInsurance()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IndexViewModel healthInsurance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.HealthInsurance.IsDuplicated(healthInsurance.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al agregar la obra social",
                            message = "Ya existe otra con el mismo nombre",
                        });
                    }
                    _workContainer.HealthInsurance.Add(healthInsurance.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = healthInsurance.CreateViewModel,
                        message = "La obra social se agregó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al agregar la obra social",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al agregar la obra social",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndexViewModel healthInsurance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.HealthInsurance.IsDuplicated(healthInsurance.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al editar la obra social",
                            message = "Ya existe otra con el mismo nombre",
                        });
                    }
                    _workContainer.HealthInsurance.Update(healthInsurance.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = healthInsurance.CreateViewModel,
                        message = "La obra social se editó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar la obra social",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar la obra social",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeState(short id)
        {
            try
            {
                var healthInsurance = _workContainer.HealthInsurance.GetOne(id);
                if (healthInsurance != null)
                {
                    _workContainer.HealthInsurance.ChangeState(id);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = healthInsurance,
                        message = "La obra social se dió de " + (healthInsurance.IsActive ? "alta" : "baja") + " correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al cambiar el estado",
                    message = "No se encontró la obra social solicitada",
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
            return Json(new { data = _workContainer.HealthInsurance.GetDropDownList() });
        }
        #endregion
    }
}
