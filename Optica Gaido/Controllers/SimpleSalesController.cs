using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.SimpleSales;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class SimpleSalesController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public SimpleSalesController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        private IActionResult CustomBadRequest(string title, string message, string error = null)
        {
            return BadRequest(new
            {
                success = false,
                title,
                message,
                error,
            });
        }

        [HttpGet]
        public IActionResult New(long? id)
        {
            try
            {
                Client client = null;
                if (id != null)
                {
                    client = _workContainer.Client.GetOne(id.Value);
                    if (client == null) return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "El cliente no existe", ErrorCode = 404 });
                }

                NewViewModel viewModel = new()
                {
                    PaymentMethods = _workContainer.PaymentMethod.GetDropDownList(),
                    Client = client ?? null,
                    Products = _workContainer.Product.GetAll()
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
        public IActionResult Create(NewViewModel viewModel)
        {
            ModelState.Remove("CreateViewModel.ClientID");
            if (ModelState.IsValid)
            {
                try
                {
                    SimpleSale sale = viewModel.CreateViewModel;
                    sale.CreatedAt = DateTime.UtcNow.AddHours(-3);

                    foreach (SimpleSaleProduct product in sale.Products)
                    {
                        Product prod = _workContainer.Product.GetOne(product.ProductID);

                        if (prod == null) return CustomBadRequest(title: "Error al crear la venta", message: "El producto ingresado no existe en los registros"); ;
                        if (product.Quantity > prod.Stock) return CustomBadRequest(title: "Error al crear la venta", message: "La cantidad ingresada es mayor al stock"); ;

                        product.SettedPrice = prod.Price;
                        product.CreatedAt = DateTime.UtcNow.AddHours(-3);
                        prod.Stock -= product.Quantity;
                    }

                    // Calcular deuda del cliente
                    if (sale.ClientID != null)
                    {
                        Client client = _workContainer.Client.GetOne(sale.ClientID.Value);
                        if (client == null) return CustomBadRequest(title: "Error al crear la venta", message: "El cliente no existe en los registros"); ;
                        decimal totalPaid = 0;
                        foreach (SimpleSalePaymentMethod pm in sale.PaymentMethods)
                        {
                            totalPaid += pm.Amount;
                        }
                        client.Debt += sale.Products.Sum(x => x.Quantity * x.SettedPrice) - totalPaid;
                        
                    }

                    _workContainer.SimpleSale.Add(sale);
                    _workContainer.Save();

                    return Json(new
                    {
                        success = true,
                        message = "La venta se realizó correctamente",
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(new
                    {
                        success = false,
                        title = "Error al crear la venta",
                        message = "Intente nuevamente o comuníquese para soporte",
                        error = e.Message,
                    });
                }
            }
            return BadRequest(new
            {
                success = false,
                title = "Error al crear la venta",
                message = "Alguno de los campos ingresados no es válido",
            });
        }
    }
}