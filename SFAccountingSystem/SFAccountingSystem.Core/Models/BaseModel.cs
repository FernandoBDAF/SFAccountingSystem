using System.ComponentModel.DataAnnotations;

namespace SFAccountingSystem.Core.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime DeletedAt { get; set; }
    }
}
