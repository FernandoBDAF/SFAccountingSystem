using SFAccountingSystem.Core.Enums;

namespace SFAccountingSystem.Core.ViewModels
{
    public class RecordOFXFilter
    {
        public int? Month { get; set; }

        public int? Year { get; set; }

        public RecordOFXBank? Bank { get; set; }
    }
}
