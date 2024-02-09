using System;

namespace SampleUserManagement.Application.Features.Roles.UpdateRole
{
	public record UpdateRoleResponse
	{
		public Guid Id { get; init; }
		public string Name { get; init; } = null!;
	}
}
