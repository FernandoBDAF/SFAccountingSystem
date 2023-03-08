using System.ComponentModel.DataAnnotations;

namespace SFAccountingSystem.Enums
{
	public enum UserEntity
	{
		[Display(Name = "Legal Entity")]
		LegalEntity = 0,

		[Display(Name = "Individual Entity")]
		IndividualEntity = 1
	}
}
