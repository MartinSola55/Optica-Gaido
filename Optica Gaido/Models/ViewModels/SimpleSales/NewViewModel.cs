using Microsoft.AspNetCore.Mvc.Rendering;

namespace Optica_Gaido.Models.ViewModels.SimpleSales
{
    public class NewViewModel
    {
        public IEnumerable<SelectListItem> PaymentMethods { get; set; } = new List<SelectListItem>();
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public Client Client { get; set; } = null;
        public SimpleSale CreateViewModel { get; set; }
    }
}
