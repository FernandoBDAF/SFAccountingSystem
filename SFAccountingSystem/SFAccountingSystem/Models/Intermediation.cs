using System.ComponentModel.DataAnnotations.Schema;

namespace SFAccountingSystem.Models
{
	public class Intermediation : BaseModel
	{
		[ForeignKey("User")]
		public int? UserId { get; set; }
		public virtual User? User { get; set; }



		public virtual ICollection<RecordOFX> RecordsOFXes { get; set; } = new HashSet<RecordOFX>();
	}
}
