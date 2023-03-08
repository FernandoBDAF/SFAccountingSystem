using System.ComponentModel.DataAnnotations;

namespace SFAccountingSystem.Enums
{
	public enum RecordOFXBank
	{
		[Display(Name = "Banco do Brasil")]
		BBPJ = 0,

		[Display(Name = "Caixa Economica Federal")]
		CEFPJ = 1
	}
}
