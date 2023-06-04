using Microsoft.AspNetCore.Mvc.Rendering;

namespace Optica_Gaido.Models.ViewModels.Sales
{
    public class NewViewModel
    {
        public IEnumerable<SelectListItem> Doctors { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Sellers { get; set; } = new List<SelectListItem>();
        public IEnumerable<SalePaymentMethod> SalePaymentMethods { get; set; } = new List<SalePaymentMethod>();
        public IEnumerable<Frame> Frames { get; set; } = new List<Frame>();
        public IEnumerable<GlassType> GlassTypes { get; set; } = new List<GlassType>();
        public IEnumerable<SelectListItem> GlassColors { get; set; } = new List<SelectListItem>();
        public Client Client { get; set; } = new Client();
        public Sale? CreateViewModel { get; set; }
    }
}
