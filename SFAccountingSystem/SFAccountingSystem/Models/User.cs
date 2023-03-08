using SFAccountingSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace SFAccountingSystem.Models
{
	public class User : BaseModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public UserEntity Entity { get; set; }

		public string? CpfCnpj { get; set; } //melhor colocar int ?

		[Required]
		public UserType Type { get; set; }



		public virtual ICollection<RecordOFX> RecordsOFXes { get; set; } = new HashSet<RecordOFX>();
	}
}
