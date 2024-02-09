using System;

namespace SampleUserManagement.Application.Features.Roles.DeleteRole
{
	public record DeleteRoleResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
