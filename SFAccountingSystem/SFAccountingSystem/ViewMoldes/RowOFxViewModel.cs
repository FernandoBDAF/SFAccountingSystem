namespace SFAccountingSystem.ViewMoldes
{
    public class RowOFxViewModel
    {
        public decimal Value { get; set; }

        public string? Description { get; set; }

        public DateTimeOffset Date { get; set; } = new DateTimeOffset();
    }
}
