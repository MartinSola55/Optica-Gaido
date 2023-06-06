using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Stats;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.Linq.Expressions;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class StatsController : Controller
    {
        private readonly IWorkContainer _workContainer;

        public StatsController(IWorkContainer workContainer)
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
                    AnnualSales = this.GetAnnualSales(DateTime.UtcNow.AddHours(-3).Year.ToString()),
                    MonthlySales = this.GetMonthlySales(DateTime.UtcNow.AddHours(-3).Year.ToString(), DateTime.UtcNow.AddHours(-3).Month.ToString()),
                    MaterialsSales = this.GetMaterialsSales(DateTime.UtcNow.AddHours(-3).Year.ToString()),
                    BrandsSales = this.GetBrandsSales(DateTime.UtcNow.AddHours(-3).Year.ToString()),
                    Years = _workContainer.Sale.GetYears()
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        [HttpGet]
        public IActionResult GetAnnualSales(string yearString)
        {
            Expression<Func<Sale, bool>> filter = sale => sale.CreatedAt.Year.ToString() == yearString;
            IEnumerable<Sale> allSales = _workContainer.Sale.GetAll(filter, hasDeletedAt: true);

            // Agrupar las ventas por mes y calcular la suma de Amount
            var salesByMonth = allSales
                .GroupBy(sale => new { sale.CreatedAt.Year, sale.CreatedAt.Month })
                .Select(group => new
                {
                    Period = $"{group.Key.Year}-{group.Key.Month.ToString().PadLeft(2, '0')}",
                    Sold = group.Sum(sale => sale.Price)
                })
                .OrderBy(entry => entry.Period)
                .ToList();

            // Construir el arreglo AnnualSales con los datos agrupados
            List<object> annualSales = new();

            for (int month = 1; month <= 12; month++)
            {
                string monthPadded = month.ToString().PadLeft(2, '0');
                string period = $"{yearString}-{monthPadded}";

                var salesEntry = salesByMonth.FirstOrDefault(entry => entry.Period == period);

                if (salesEntry != null) annualSales.Add(salesEntry);
                else annualSales.Add(new { period, sold = 0 });
            }
            return Json(new
            {
                success = true,
                data = annualSales,
            });
        }

        [HttpGet]
        public IActionResult GetMonthlySales(string yearString, string monthString)
        {
            Expression<Func<Sale, bool>> filter = sale => sale.CreatedAt.Year.ToString() == yearString && sale.CreatedAt.Month.ToString() == monthString;
            IEnumerable<Sale> allSales = _workContainer.Sale.GetAll(filter, hasDeletedAt: true);

            // Convertir los parámetros de cadena a valores numéricos
            int year = int.Parse(yearString);
            int month = int.Parse(monthString);

            // Agrupar las ventas por día y calcular la suma de Amount
            var salesByDay = allSales
                .GroupBy(sale => sale.CreatedAt.Day)
                .Select(group => new
                {
                    Day = group.Key,
                    Sold = group.Sum(sale => sale.Price)
                })
                .OrderBy(entry => entry.Day)
                .ToList();

            // Construir el arreglo monthlySales con los datos agrupados
            List<object> monthlySales = new();
            for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
            {
                var sale = salesByDay.FirstOrDefault(s => s.Day == day);
                var sold = sale != null ? sale.Sold : 0;

                string monthPadded = month.ToString().PadLeft(2, '0');
                string dayPadded = day.ToString().PadLeft(2, '0');
                string period = $"{yearString}-{monthPadded}-{dayPadded}";

                var dailySaleObject = new { period, sold };

                monthlySales.Add(dailySaleObject);
            }

            return Json(new
            {
                success = true,
                data = monthlySales,
            });
        }

        [HttpGet]
        public IActionResult GetMaterialsSales(string yearString)
        {
            Expression<Func<Sale, bool>> filter = sale => sale.CreatedAt.Year.ToString() == yearString;
            IEnumerable<Sale> allSales = _workContainer.Sale.GetAll(filter, includeProperties: "Frame, Frame.Material", hasDeletedAt: true);

            var materialSales = allSales
                .GroupBy(sale => sale.Frame.Material.ID)
                .Select(group => new
                {
                    brandID = group.Key,
                    material = group.First().Frame.Material.Description,
                    sold = group.Count()
                })
                .ToList();

            dynamic dataObject = new ExpandoObject();
            dataObject.period = yearString;

            foreach (var item in materialSales)
            {
                ((IDictionary<string, object>)dataObject)[item.material] = item.sold;
            }

            return Json(new
            {
                success = true,
                data = dataObject
            });
        }


        [HttpGet]
        public IActionResult GetBrandsSales(string yearString)
        {
            Expression<Func<Sale, bool>> filter = sale => sale.CreatedAt.Year.ToString() == yearString;
            IEnumerable<Sale> allSales = _workContainer.Sale.GetAll(filter, includeProperties: "Frame, Frame.Brand", hasDeletedAt: true);

            var brandSales = allSales
                .GroupBy(sale => sale.Frame.Brand.ID)
                .Select(group => new
                {
                    brandID = group.Key,
                    brand = group.First().Frame.Brand.Name,
                    sold = group.Count()
                })
                .ToList();

            dynamic dataObject = new ExpandoObject();
            dataObject.period = yearString;

            foreach (var item in brandSales)
            {
                ((IDictionary<string, object>)dataObject)[item.brand] = item.sold;
            }

            return Json(new
            {
                success = true,
                data = dataObject
            });
        }
    }
}
