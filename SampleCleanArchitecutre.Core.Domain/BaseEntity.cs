using System.ComponentModel.DataAnnotations;

namespace SampleCleanArchitecture.Core.Domain
{
    public class AuditableBaseEntity
    {
        [Key]
        public Ulid Id { get; set; }

        [Required]
        public DateTime CreatedTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;


    }
}