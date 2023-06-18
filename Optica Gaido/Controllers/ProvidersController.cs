using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Providers;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class ProvidersController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public ProvidersController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

#region Providers

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IndexViewModel viewModel = new();
                foreach (var provider in _workContainer.Provider.GetAll())
                {
                    IEnumerable<Debt> debts = _workContainer.Debt.GetProviderDebts(provider.ID);

                    // Calcular la deuda del proveedor sumando los precios de todas las deudas
                    decimal totalDebt = debts.Sum(x => x.Price);

                    // Obtener todos los pagos de deudas correspondientes a las deudas del proveedor
                    List<long> debtsIDs = debts.Select(d => d.ID).ToList();
                    decimal totalPayments = _workContainer.DebtPayment.GetAllPayments(debtsIDs).Sum(x => x.Amount);

                    decimal providerDebt = totalDebt - totalPayments;

                    // Agregar el proveedor al ViewModel
                    viewModel.Providers.Add((provider, providerDebt));
                }
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
                        message = "El proveedor se agregó correctamente",
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
                    title = "Error al eliminar",
                    message = "Intente nuevamente o comuníquese para soporte",
                    error = e.Message,
                });
            }
        }
#endregion

#region Deudas

        [HttpGet]
        public IActionResult Debts(long id)
        {
            try
            {
                Provider provider = _workContainer.Provider.GetOne(id);
                if (provider == null)
                {
                    return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "El proveedor no existe", ErrorCode = 404 });
                }
                DebtsViewModel viewModel = new()
                {
                    Provider = provider,
                    Debts = _workContainer.Debt.GetProviderDebts(id)
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDebt(DebtsViewModel debt)
        {
            ModelState.Remove("CreateViewModel.ID");
            if (ModelState.IsValid)
            {
                try
                {
                    debt.CreateViewModel.CreatedAt = DateTime.UtcNow.AddHours(-3);
                    _workContainer.Debt.Add(debt.CreateViewModel);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = debt.CreateViewModel,
                        message = "La deuda se agregó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al agregar la deuda",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al agregar la deuda",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PayDebt(DebtsViewModel payment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Debt debt = _workContainer.Debt.GetOne(payment.DebtPayment.DebtID);
                    if (payment.DebtPayment.Amount > debt.Price)
                    {
                        return BadRequest(new
                        {
                            success = false,
                            title = "Error al pagar la deuda",
                            message = "El monto ingresado es mayor al de la deuda",
                        });
                    }
                    payment.DebtPayment.CreatedAt = DateTime.UtcNow.AddHours(-3);
                    _workContainer.DebtPayment.Add(payment.DebtPayment);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = payment.DebtPayment,
                        message = "El pago se realizó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al pagar la deuda",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al pagar la deuda",
                message = "Alguno de los campos ingresados no es válido",
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDeleteDebt(long id)
        {
            try
            {
                var debt = _workContainer.Debt.GetOne(id);
                if (debt != null)
                {
                    _workContainer.Debt.SoftDelete(id);
                    _workContainer.Save();
                    return Json(new
                    {
                        success = true,
                        data = id,
                        message = "La deuda se eliminó correctamente",
                    });
                }
                return BadRequest(new
                {
                    success = false,
                    title = "Error al eliminar",
                    message = "No se encontró la deuda solicitada",
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

#endregion

    }
}
