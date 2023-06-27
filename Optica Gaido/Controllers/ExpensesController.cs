using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Expenses;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public ExpensesController(IWorkContainer workContainer)
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
                    Expenses = _workContainer.Expense.GetAll(hasDeletedAt: true),
                    CreateViewModel = new Expense()
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
        public IActionResult Create(IndexViewModel expense)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    expense.CreateViewModel.CreatedAt = DateTime.UtcNow.AddHours(-3);
                    _workContainer.Expense.Add(expense.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = expense.CreateViewModel,
                        message = "El gasto se creó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al crear el gasto",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al crear el gasto",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndexViewModel expense)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _workContainer.Expense.Update(expense.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = expense.CreateViewModel,
                        message = "El gasto se editó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar el gasto",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar el gasto",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(long id)
        {
            try
            {
                if (_workContainer.Expense.GetOne(id) != null)
                {
                    _workContainer.Expense.SoftDelete(id);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = id,
                        message = "El gasto se eliminó correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al eliminar",
                    message = "No se encontró el gasto solicitado",
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
