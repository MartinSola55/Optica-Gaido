using Microsoft.AspNetCore.Mvc.Rendering;

namespace Optica_Gaido.Models.ViewModels.Clients
{
    public class IndexViewModel
    {
        public IEnumerable<Client> Clients { get; set; } = new List<Client>();
        public IEnumerable<SelectListItem> HealthInsurances { get; set; } = new List<SelectListItem>();
        public Client CreateViewModel { get; set; } = new Client();
    }
}
