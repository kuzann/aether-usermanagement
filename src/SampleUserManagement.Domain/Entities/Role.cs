using SampleUserManagement.Domain.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleUserManagement.Domain.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        public string Name { get; set; } = null!;

        public virtual ICollection<User>? Users { get; set; }
    }
}
