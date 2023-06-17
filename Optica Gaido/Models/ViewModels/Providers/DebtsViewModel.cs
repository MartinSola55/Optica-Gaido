namespace Optica_Gaido.Models.ViewModels.Providers
{
    public class DebtsViewModel
    {
        public Provider Provider { get; set; }
        public Debt CreateViewModel { get; set; }
        public DebtPayment DebtPayment { get; set; }
        public IEnumerable<Debt> Debts = new List<Debt>();
    }
}
