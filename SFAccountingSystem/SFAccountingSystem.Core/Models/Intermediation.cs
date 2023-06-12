using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace SFAccountingSystem.Core.Models
{
    public class Intermediation : BaseModel
    {
        [ForeignKey("User")]
        public int? UserId { get; set; }

        public virtual User? User { get; set; }

        public virtual ICollection<RecordOFX> RecordsOFXes { get; set; } = new HashSet<RecordOFX>();

        [NotMapped]
        public List<int> RecordOfxIds { get; set; } = new List<int>();

        [Precision(18, 2)]
        public decimal Value { get; set; }

        public string? InvoiceUserIds { get; set; }

        [NotMapped]
        public List<int> UserIds { get; set; } = new List<int>();

        public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    }
}
