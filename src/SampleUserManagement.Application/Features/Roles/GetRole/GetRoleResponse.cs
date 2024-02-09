using System;

namespace SampleUserManagement.Application.Features.Roles.GetRole
{
	public record GetRoleResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
