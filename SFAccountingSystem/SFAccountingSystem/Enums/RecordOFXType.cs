using System.ComponentModel.DataAnnotations;

namespace SFAccountingSystem.Enums
{
	public enum RecordOFXType
	{
		[Display(Name = "Credit")]
		Credit = 0,

		[Display(Name = "Debit")]
		Debit = 1
	}
}
