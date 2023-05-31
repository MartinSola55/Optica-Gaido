namespace Optica_Gaido.Models.ViewModels.Expenses
{
    public class IndexViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; } = new List<Expense>();
        public Expense CreateViewModel { get; set; } = new Expense();
    }
}
