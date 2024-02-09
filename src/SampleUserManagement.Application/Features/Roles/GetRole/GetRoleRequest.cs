using MediatR;
using System;

namespace SampleUserManagement.Application.Features.Roles.GetRole
{
    public record GetRoleRequest(Guid Id) : IRequest<GetRoleResponse>;
}
