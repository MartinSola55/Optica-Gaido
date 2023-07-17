using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Frames;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class FramesController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public FramesController(IWorkContainer workContainer)
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
                    Frames = _workContainer.Frame.GetAll(includeProperties: "Brand, Material"),
                    Brands = _workContainer.Brand.GetDropDownList(),
                    Materials = _workContainer.Material.GetDropDownList(),
                    CreateViewModel = new Frame()
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
        public IActionResult Create(IndexViewModel frame)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Frame.IsDuplicated(frame.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al agregar el marco",
                            message = "Ya existe otro con el mismo código de modelo",
                        });
                    }
                    _workContainer.Frame.Add(frame.CreateViewModel);
                    _workContainer.Save();
                    Expression<Func<Frame, bool>> filter = sale => sale.ID == frame.CreateViewModel.ID;
                    Frame newFrame = _workContainer.Frame.GetFirstOrDefault(filter, includeProperties: "Brand, Material");
                    return Json(new
                    {
                        success = true,
                        data = newFrame,
                        message = "El marco se agregó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al agregar el marco",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al agregar el marco",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndexViewModel updatedFrame)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Frame.IsDuplicated(updatedFrame.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al editar el marco",
                            message = "Ya existe otro con el mismo código de modelo",
                        });
                    }
                    _workContainer.Frame.Update(updatedFrame.CreateViewModel);
                    _workContainer.Save();
                    Frame frame = _workContainer.Frame.GetFirstOrDefault(x => x.ID == updatedFrame.CreateViewModel.ID, includeProperties: "Brand, Material");
                    return Json(new
                    {
                        success = true,
                        data = frame,
                        message = "El marco se editó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar el marco",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar el marco",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(long id)
        {
            try
            {
                var frame = _workContainer.Frame.GetOne(id);
                if (frame != null)
                {
                    _workContainer.Frame.SoftDelete(id);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = id,
                        message = "El marco se eliminó correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al eliminar",
                    message = "No se encontró el marco solicitado",
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
