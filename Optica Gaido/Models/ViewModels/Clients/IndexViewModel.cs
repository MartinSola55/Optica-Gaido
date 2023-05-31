using Microsoft.AspNetCore.Mvc.Rendering;

namespace Optica_Gaido.Models.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<Client> Clients { get; set; } = new List<Client>();
    }
}
