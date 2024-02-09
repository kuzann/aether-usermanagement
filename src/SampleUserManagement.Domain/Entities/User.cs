using SampleUserManagement.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleUserManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; } = null!;

		[Required]
        public string Password { get; set; } = null!;

		public string? FullName { get; set; }
		public DateOnly? DateOfBirth { get; set; }

		public virtual ICollection<Role>? Roles { get; set; }
    }
}
