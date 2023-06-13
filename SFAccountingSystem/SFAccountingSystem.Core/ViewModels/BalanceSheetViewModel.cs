namespace SFAccountingSystem.Core.ViewModels
{
    public class BalanceSheetViewModel
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public ICollection<BalanceSheetViewModel> Children { get; set; } = new List<BalanceSheetViewModel>();
    }
}
