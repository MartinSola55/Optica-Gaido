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
    public class HealthInsurances : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly ApplicationDbContext _context;

        public HealthInsurances(IWorkContainer workContainer, ApplicationDbContext context)
        {
            _context = context;
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel
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
        public IActionResult Edit([Bind("ID,Name")] HealthInsurance healthInsurance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _workContainer.HealthInsurance.Update(healthInsurance);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = healthInsurance,
                        message = "La obra social se agregó correctamente",
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
                title = "Error al agregar la obra social",
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
                    string state = healthInsurance.IsActive ? "alta" : "baja";
                    return Json(new
                    {
                        success = true,
                        data = healthInsurance,
                        message = "La obra social se dió de " + state + " correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al eliminar la obra social",
                    message = "No se encontró la obra social solicitada",
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    success = false,
                    title = "Error al eliminar la obra social",
                    message = "Intente nuevamente o comuníquese para soporte",
                    error = e.Message,
                });
            }
        }

        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workContainer.HealthInsurance.GetAll() });
        }
        #endregion
    }
}
