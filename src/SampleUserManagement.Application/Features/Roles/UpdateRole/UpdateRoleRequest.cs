using MediatR;
using System;

namespace SampleUserManagement.Application.Features.Roles.UpdateRole
{
	public record UpdateRoleRequest(Guid Id, string Name) : IRequest<UpdateRoleResponse>;
}
