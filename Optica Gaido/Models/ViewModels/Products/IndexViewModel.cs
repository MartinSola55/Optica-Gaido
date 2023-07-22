namespace Optica_Gaido.Models.ViewModels.Products
{
    public class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public Product CreateViewModel { get; set; } = new Product();
    }
}
