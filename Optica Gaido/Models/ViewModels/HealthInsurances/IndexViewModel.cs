namespace Optica_Gaido.Models.ViewModels.HealthInsurances
{
    public class IndexViewModel
    {
        public IEnumerable<HealthInsurance> HealthInsurances { get; set; } = new List<HealthInsurance>();
        public HealthInsurance CreateViewModel { get; set; } = new HealthInsurance();
    }
}
