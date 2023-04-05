﻿using SFAccountingSystem.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFAccountingSystem.Core.Models
{
    public class RecordOFX : BaseModel
    {
        public RecordOFX() { }

        public RecordOFX(TransactionOFX transaction)
        {
            Value = transaction.Value;
            Details = transaction.Description;
            Date = transaction.Date.DateTime;
        }

        public DateTime Date { get; set; }

        public string? Details { get; set; }

        public decimal Value { get; set; }

        public RecordOFXBank Bank { get; set; }

        public RecordOFXType Type { get; set; }



        [ForeignKey("Intermediation")]
        public int? IntermediationId { get; set; }
        public virtual Intermediation? Intermediation { get; set; }



        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User? User { get; set; }



        public RecordOFXGroup? Group { get; set; }




        [ForeignKey("RecordOFXSubGroup")]
        public int? SubGroupId { get; set; }
        public virtual RecordOFXSubGroup? RecordOFXSubGroup { get; set; }
    }
}