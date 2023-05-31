using Microsoft.AspNetCore.Mvc.Rendering;

namespace Optica_Gaido.Models.ViewModels.Sales
{
    public class NewViewModel
    {
        public IEnumerable<SelectListItem> Doctors { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Sellers { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> PaymentMethods { get; set; } = new List<SelectListItem>();
        public IEnumerable<GlassFormat> GlassFormats { get; set; } = new List<GlassFormat>();
        public GlassType GlassType { get; set; } = new GlassType();
        public GlassColor GlassColor { get; set; } = new GlassColor();
        public Client Client { get; set; } = new Client();
        public Sale CreateViewModel { get; set; } = new Sale();
    }
}
