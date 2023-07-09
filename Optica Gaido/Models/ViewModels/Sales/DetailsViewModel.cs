using Microsoft.AspNetCore.Mvc.Rendering;

namespace Optica_Gaido.Models.ViewModels.Sales
{
    public class DetailsViewModel
    {
        public Sale Sale { get; set; }
        public IEnumerable<SelectListItem> GlassColors { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> GlassFocusTypes { get; set; } = new List<SelectListItem>();
        public IEnumerable<GlassType> GlassTypes { get; set; } = new List<GlassType>();
        public IEnumerable<SelectListItem> Doctors { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Sellers { get; set; } = new List<SelectListItem>();
        public IEnumerable<SalePaymentMethod> SalePaymentMethods { get; set; } = new List<SalePaymentMethod>();
    }
}
