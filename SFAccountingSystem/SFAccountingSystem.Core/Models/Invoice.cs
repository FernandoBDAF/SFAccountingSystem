using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFAccountingSystem.Core.Models
{
    public class Invoice : BaseModel
    {
        [ForeignKey("Intermediation")]
        public int? IntermediationId { get; set; }

        public virtual Intermediation? Intermediation { get; set; }

        [ForeignKey("RecordOfx")]
        public int? RecordOfxId { get; set; }

        public virtual RecordOFX? RecordOfx { get; set; }

        public string? NrNumber { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime? Date { get; set; }

        [Precision(18, 2)]
        public decimal Value { get; set; }

        public DateTime? ApprovedAt { get; set; }
    }
}
