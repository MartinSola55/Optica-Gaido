using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Optica_Gaido.Models;

namespace Optica_Gaido.Views.HealthInsurances
{
    public class IndexModel : PageModel
    {
        public IEnumerable<HealthInsurance> HealthInsurances { get; set; }
        public HealthInsurance HealthInsuranceEditable { get; set; }
        public void OnGet()
        {

        }
    }
}
