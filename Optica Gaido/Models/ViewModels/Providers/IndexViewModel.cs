namespace Optica_Gaido.Models.ViewModels.Providers
{
    public class IndexViewModel
    {
        public IEnumerable<Provider> Providers { get; set; } = new List<Provider>();
        public Provider CreateViewModel { get; set; } = new Provider();
    }
}
