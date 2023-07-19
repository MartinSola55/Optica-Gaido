using Microsoft.AspNetCore.Mvc.Rendering;

namespace Optica_Gaido.Models.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<Client> Clients { get; set; } = new List<Client>();
        public decimal MonthlyEarnings { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public decimal ProvidersDebts { get; set; }
        public int TotalSales { get; set; }
        public string AmountToPay { get; set; }
        public List<SalePaymentMethod> PaymentMethods { get; set; } = new List<SalePaymentMethod>();
    }
}
