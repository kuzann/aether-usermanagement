using System;

namespace SampleUserManagement.Application.Features.Roles.FilterRole
{
    public record FilterRoleResponse
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
    }
}
