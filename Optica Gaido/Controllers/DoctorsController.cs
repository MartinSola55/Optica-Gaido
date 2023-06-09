﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Doctors;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class DoctorsController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public DoctorsController(IWorkContainer workContainer)
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
                    Doctors = _workContainer.Doctor.GetAll(),
                    CreateViewModel = new Doctor()
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
        public IActionResult Create(IndexViewModel doctor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Doctor.IsDuplicated(doctor.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al agregar el doctor",
                            message = "Ya existe otro con la misma licencia y/o el mismo nombre y apellido",
                        });
                    }
                    _workContainer.Doctor.Add(doctor.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = doctor.CreateViewModel,
                        message = "El doctor se agregó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al agregar el doctor",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al agregar el doctor",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndexViewModel doctor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Doctor.IsDuplicated(doctor.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al editar el doctor",
                            message = "Ya existe otro con la misma licencia y/o el mismo nombre y apellido",
                        });
                    }
                    _workContainer.Doctor.Update(doctor.CreateViewModel);
                    _workContainer.Save();
                    Doctor editedDoctor = _workContainer.Doctor.GetOne(doctor.CreateViewModel.ID);
                    return Json(new
                    {
                        success = true,
                        data = editedDoctor,
                        message = "El doctor se editó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar el doctor",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar el doctor",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeState(long id)
        {
            try
            {
                var doctor = _workContainer.Doctor.GetOne(id);
                if (doctor != null)
                {
                    _workContainer.Doctor.ChangeState(id);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = doctor,
                        message = "El doctor se dió de " + (doctor.IsActive ? "alta" : "baja") + " correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al cambiar el estado",
                    message = "No se encontró el doctor solicitado",
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

        #endregion
    }
}
