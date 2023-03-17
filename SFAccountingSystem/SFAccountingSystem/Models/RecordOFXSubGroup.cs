using SFAccountingSystem.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFAccountingSystem.Models
{
	public class RecordOFXSubGroup : BaseModel //faltou required em algumas propriedades
	{
		public RecordOFXGroup Group { get; set; }

		public string Description { get; set; }



		public virtual IEnumerable<RecordOFX> RecordsOFXes { get; set; } = new HashSet<RecordOFX>();



		[ForeignKey("Parent")]
		public int? ParentId { get; set; }
		public virtual RecordOFXSubGroup? Parent { get; set; }

		public virtual IEnumerable<RecordOFXSubGroup> ChildRecordsOFXes { get; set; } = new HashSet<RecordOFXSubGroup>();
	}
}
