using System;

namespace SampleUserManagement.Application.Features.Roles.CreateRole
{
    public record CreateRoleResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
    }
}
