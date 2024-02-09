using MediatR;
using System;

namespace SampleUserManagement.Application.Features.Roles.DeleteRole
{
    public record DeleteRoleRequest(Guid Id) : IRequest<DeleteRoleResponse>;
}
