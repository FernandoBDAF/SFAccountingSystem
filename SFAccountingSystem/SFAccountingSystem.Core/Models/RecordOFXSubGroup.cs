using SFAccountingSystem.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFAccountingSystem.Core.Models
{
    public class RecordOFXSubGroup : BaseModel //faltou required em algumas propriedades
    {
        [Required]
        public RecordOFXGroup Group { get; set; }

        [Required]
        public string? Description { get; set; }



        public virtual IEnumerable<RecordOFX> RecordsOFXes { get; set; } = new HashSet<RecordOFX>();



        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
        public virtual RecordOFXSubGroup? Parent { get; set; }

        public virtual IEnumerable<RecordOFXSubGroup> ChildRecordsOFXes { get; set; } = new HashSet<RecordOFXSubGroup>();

        public bool CanDelete
        {
            get
            {
                return RecordsOFXes.Count() == 0 && ChildRecordsOFXes.Count() == 0;
            }
        }
    }
}
