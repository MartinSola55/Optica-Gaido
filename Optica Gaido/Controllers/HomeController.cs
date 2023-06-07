using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Home;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public HomeController(IWorkContainer workContainer)
        {
            _workContainer = workContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                Expression<Func<Sale, bool>> filterSale = sale => sale.CreatedAt.Year == DateTime.UtcNow.AddHours(-3).Year && sale.CreatedAt.Month == DateTime.UtcNow.AddHours(-3).Month;
                Expression<Func<Expense, bool>> filterExpense = expense => expense.CreatedAt.Year == DateTime.UtcNow.AddHours(-3).Year && expense.CreatedAt.Month == DateTime.UtcNow.AddHours(-3).Month;
                var sales = _workContainer.Sale.GetAll(filterSale, hasDeletedAt: true);
                int totalSales = sales.Count();
                decimal monthlyEarnings = sales.Sum(x => x.Price);
                decimal monthlyExpenses = _workContainer.Expense.GetAll(filterExpense).Sum(x => x.Amount);

                IndexViewModel viewModel = new()
                {
                    Clients = _workContainer.Client.GetAll(includeProperties: "HealthInsurance"),
                    MonthlyEarnings = monthlyEarnings,
                    MonthlyExpenses = monthlyExpenses,
                    TotalSales = totalSales,
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}