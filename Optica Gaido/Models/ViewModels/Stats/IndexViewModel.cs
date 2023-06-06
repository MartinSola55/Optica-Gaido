using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Optica_Gaido.Models.ViewModels.Stats
{
    public class IndexViewModel
    {
        public IActionResult AnnualSales { get; set; }
        public IActionResult MonthlySales { get; set; }
        public IActionResult MaterialsSales { get; set; }
        public IActionResult BrandsSales { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; } = new List<SelectListItem>();
        //public int AnualSales { get; set; }
    }
}