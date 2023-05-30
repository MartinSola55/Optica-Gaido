namespace Optica_Gaido.Models.ViewModels.Brands
{
    public class IndexViewModel
    {
        public IEnumerable<Brand> Brands { get; set; } = new List<Brand>();
        public Brand CreateViewModel { get; set; } = new Brand();
    }
}
