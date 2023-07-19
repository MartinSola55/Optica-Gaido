using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Clients;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public ClientsController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<Client> clients = _workContainer.Client.GetAll(includeProperties: "HealthInsurance").ToList();

                foreach (Client client in clients)
                {
                    client.LastSaleDate = _workContainer.Sale.GetLastSale(client.ID);
                }

                IndexViewModel viewModel = new()
                {
                    Clients = clients,
                    HealthInsurances = _workContainer.HealthInsurance.GetDropDownList(),
                    CreateViewModel = new Client()
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
        public IActionResult Create(IndexViewModel client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Client.IsDuplicated(client.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al agregar el cliente",
                            message = "Ya existe otro con el mismo nombre, apellido y dirección",
                        });
                    }
                    client.CreateViewModel.CreatedAt = DateTime.UtcNow.AddHours(-3);
                    _workContainer.Client.Add(client.CreateViewModel);
                    _workContainer.Save();

                    short? HI_ID = client.CreateViewModel.HealthInsuranceID;
                    if (HI_ID != null)
                    {
                        client.CreateViewModel.HealthInsurance = _workContainer.HealthInsurance.GetOne(HI_ID.Value);
                    }

                    return Json(new
                    {
                        success = true,
                        data = client.CreateViewModel,
                        message = "El cliente se agregó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al agregar el cliente",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al agregar el cliente",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndexViewModel client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_workContainer.Client.IsDuplicated(client.CreateViewModel))
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al editar el cliente",
                            message = "Ya existe otro con el mismo nombre, apellido y dirección",
                        });
                    }
                    _workContainer.Client.Update(client.CreateViewModel);
                    _workContainer.Save();

                    short? HI_ID = client.CreateViewModel.HealthInsuranceID;
                    if (HI_ID != null)
                    {
                        client.CreateViewModel.HealthInsurance = _workContainer.HealthInsurance.GetOne(HI_ID.Value);
                    }

                    return Json(new
                    {
                        success = true,
                        data = client.CreateViewModel,
                        message = "El cliente se editó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al editar el cliente",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al editar el cliente",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeState(long id)
        {
            try
            {
                var client = _workContainer.Client.GetOneWithProperties(id, properties: "HealthInsurance");
                if (client != null)
                {
                    _workContainer.Client.ChangeState(id);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = client,
                        message = "El cliente se dió de " + (client.IsActive ? "alta" : "baja") + " correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al cambiar el estado",
                    message = "No se encontró el cliente solicitado",
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
    }
}
