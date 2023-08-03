using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Optica_Gaido.Data.Repository.IRepository;
using Optica_Gaido.Models;
using Optica_Gaido.Models.ViewModels.Home;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;

namespace Optica_Gaido.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IWorkContainer _workContainer;
        private readonly IConfiguration _config;

        public HomeController(IWorkContainer workContainer, IConfiguration config)
        {
            _workContainer = workContainer;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                DateTime today = DateTime.UtcNow.AddHours(-3);
                Expression<Func<Sale, bool>> filterSale = sale => sale.CreatedAt.Year == today.Year && sale.CreatedAt.Month == today.Month;
                Expression<Func<SimpleSale, bool>> filterSimpleSale = sale => sale.CreatedAt.Year == today.Year && sale.CreatedAt.Month == today.Month;
                Expression<Func<Expense, bool>> filterExpense = expense => expense.CreatedAt.Year == today.Year && expense.CreatedAt.Month == today.Month;

                int totalSales = _workContainer.Sale.GetAll(filterSale, includeProperties: "SalePaymentMethods").ToList().Count;
                List<SimpleSale> simpleSales = _workContainer.SimpleSale.GetAll(filterSimpleSale, includeProperties: "PaymentMethods").ToList();

                decimal monthlyEarnings = 0;
                List<SalePaymentMethod> paymentMethods = new();

                // Recorrer las ventas con aumento
                List <SalePaymentMethod> monthMethods = _workContainer.SalePaymentMethod.GetAll().ToList();
                foreach (SalePaymentMethod pm in monthMethods.Where(x => x.UpdatedAt.Month == today.Month && x.UpdatedAt.Year == today.Year))
                {
                    monthlyEarnings += pm.Amount;
                    // Verificar si el metodo de pago ya existe y sumar el total de cada metodo de pago
                    if (paymentMethods.Any(x => x.PaymentMethodID == pm.PaymentMethodID))
                    {
                        paymentMethods.Find(x => x.PaymentMethodID == pm.PaymentMethodID).Amount += pm.Amount;
                    }
                    else
                    {
                        paymentMethods.Add(new SalePaymentMethod { Amount = pm.Amount, PaymentMethodID = pm.PaymentMethodID });
                    }
                }

                // Recorrer las ventas de productos
                foreach (SimpleSale sale in simpleSales)
                {
                    foreach (SimpleSalePaymentMethod pm in sale.PaymentMethods)
                    {
                        monthlyEarnings += pm.Amount;
                        // Verificar si el metodo de pago ya existe y sumar el total de cada metodo de pago
                        if (paymentMethods.Any(x => x.PaymentMethodID == pm.PaymentMethodID))
                        {
                            paymentMethods.Find(x => x.PaymentMethodID == pm.PaymentMethodID).Amount += pm.Amount;
                        }
                        else
                        {
                            paymentMethods.Add(new SalePaymentMethod { Amount = pm.Amount, PaymentMethodID = pm.PaymentMethodID });
                        }
                    }
                }

                // Asignar el método de pago correspondiente
                foreach (SalePaymentMethod pm in paymentMethods)
                {
                    pm.PaymentMethod = _workContainer.PaymentMethod.GetOne(pm.PaymentMethodID);
                }

                decimal monthlyExpenses = _workContainer.Expense.GetAll(filterExpense).Sum(x => x.Amount);
                decimal providerDebts = _workContainer.Debt.GetAll().Sum(x => x.Price) - _workContainer.DebtPayment.GetAll().Sum(x => x.Amount);

                string payment = null;
                if (DateTime.UtcNow.AddHours(-3).Day == 9)
                {
                    API_Obj currency = await Import(_config["USD_API_KEY"]);
                    if (currency.Result == "success")
                    {
                        payment = ((currency.Conversion_rates.ARS * 35) * 2).ToString("N0", new System.Globalization.CultureInfo("is-IS"));
                    }
                }

                IndexViewModel viewModel = new()
                {
                    Clients = _workContainer.Client.GetAll(includeProperties: "HealthInsurance", hasIsActive: true),
                    MonthlyEarnings = monthlyEarnings,
                    MonthlyExpenses = monthlyExpenses,
                    TotalSales = totalSales,
                    ProvidersDebts = providerDebts,
                    AmountToPay = payment,
                    PaymentMethods = paymentMethods
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("~/Views/Error.cshtml", new ErrorViewModel { Message = "Ha ocurrido un error inesperado con el servidor\nSi sigue obteniendo este error contacte a soporte", ErrorCode = 500 });
            }
        }

        private static async Task<API_Obj> Import(string usd_api_key)
        {
            try
            {
                string URLString = "https://v6.exchangerate-api.com/v6/" + usd_api_key + "/latest/USD";

                using HttpClient client = new();
                var response = await client.GetAsync(URLString);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    API_Obj Test = JsonConvert.DeserializeObject<API_Obj>(json);
                    return Test;
                }
                else
                {
                    // Si la solicitud no fue exitosa, puedes manejar el error aquí
                    return new API_Obj();
                }
            }
            catch (Exception)
            {
                return new API_Obj();
            }
        }

        private class API_Obj
        {
            public string Result { get; set; }
            public string Documentation { get; set; }
            public string Terms_of_use { get; set; }
            public string Time_last_update_unix { get; set; }
            public string Time_last_update_utc { get; set; }
            public string Time_next_update_unix { get; set; }
            public string Time_next_update_utc { get; set; }
            public string Base_code { get; set; }
            public ConversionRate Conversion_rates { get; set; }
        }

        private class ConversionRate
        {
            public double ARS { get; set; }
        }

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}