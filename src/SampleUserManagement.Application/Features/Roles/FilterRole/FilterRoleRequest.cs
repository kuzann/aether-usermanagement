using MediatR;
using System.Collections.Generic;

namespace SampleUserManagement.Application.Features.Roles.FilterRole
{
    public record FilterRoleRequest(string? Name) : IRequest<List<FilterRoleResponse>>;
}
