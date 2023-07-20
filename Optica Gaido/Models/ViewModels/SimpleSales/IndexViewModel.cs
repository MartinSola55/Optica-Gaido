using Microsoft.AspNetCore.Mvc.Rendering;

namespace Optica_Gaido.Models.ViewModels.SimpleSales
{
    public class IndexViewModel
    {
        public IEnumerable<SimpleSale> Sales { get; set; } = new List<SimpleSale>();
        public IEnumerable<SelectListItem> Years { get; set; } = new List<SelectListItem>();
    }
}
