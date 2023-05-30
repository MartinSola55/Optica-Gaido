namespace Optica_Gaido.Models.ViewModels.Materials
{
    public class IndexViewModel
    {
        public IEnumerable<Material> Materials { get; set; } = new List<Material>();
        public Material CreateViewModel { get; set; } = new Material();
    }
}
