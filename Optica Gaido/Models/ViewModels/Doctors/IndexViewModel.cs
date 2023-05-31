
namespace Optica_Gaido.Models.ViewModels.Doctors
{
    public class IndexViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; } = new List<Doctor>();
        public Doctor CreateViewModel { get; set; } = new Doctor();
    }
}
