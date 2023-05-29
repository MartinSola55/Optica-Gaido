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
              return View(_workContainer.HealthInsurance.GetAll());
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] HealthInsurance healthInsurance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _workContainer.HealthInsurance.Add(healthInsurance);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(short id)
        {
            try
            {
                var healthInsurance = _workContainer.HealthInsurance.GetOne(id);
                if (healthInsurance != null)
                {
                    _workContainer.HealthInsurance.Remove(healthInsurance);
                    _workContainer.Save();
                }
                return Json(new
                {
                    success = true,
                    data = id,
                    message = "La obra social se eliminó correctamente",
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

        private bool HealthInsuranceExists(short id)
        {
          return (_context.HealthInsurances?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
