using System.ComponentModel.DataAnnotations;

namespace SampleUserManagement.Base.Entities
{
    public abstract class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string CreatedBy { get; set; } = null!;

		[Required]
        public DateTimeOffset CreatedAt { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedAt { get; set; }

        public string? DeletedBy { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }
    }
}
