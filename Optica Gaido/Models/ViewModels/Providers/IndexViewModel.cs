namespace Optica_Gaido.Models.ViewModels.Providers
{
    public class IndexViewModel
    {
        public List<(Provider, decimal)> Providers { get; set; } = new();
        public Provider CreateViewModel { get; set; } = new Provider();
    }
}
