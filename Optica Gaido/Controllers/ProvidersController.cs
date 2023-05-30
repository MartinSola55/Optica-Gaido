using Microsoft.AspNetCore.Mvc;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Providers;

namespace Optica_Gaido.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public ProvidersController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IndexViewModel viewModel = new()
            {
                Providers = _workContainer.Provider.GetAll(),
                CreateViewModel = new Provider()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IndexViewModel provider)
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
                            title = "Error al agregar el proveedor",
                            message = "Ya existe otro con el mismo nombre y apellido",
                        });
                    }
                    _workContainer.Provider.Add(provider.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = provider.CreateViewModel,
                        message = "El provider se agregó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al agregar el proveedor",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al agregar el proveedor",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
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
        }

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
                    title = "Error al eliminarr",
                    message = "Intente nuevamente o comuníquese para soporte",
                    error = e.Message,
                });
            }
        }

        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetDropDownList()
        {
            return Json(new { data = _workContainer.Provider.GetDropDownList() });
        }
        #endregion
    }
}
