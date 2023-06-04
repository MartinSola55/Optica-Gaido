using Microsoft.AspNetCore.Mvc.Rendering;

namespace Optica_Gaido.Models.ViewModels.Sales
{
    public class IndexViewModel
    {
        public IEnumerable<Sale> Sales { get; set; } = new List<Sale>();
        public IEnumerable<SelectListItem> Years { get; set; } = new List<SelectListItem>();
    }
}
