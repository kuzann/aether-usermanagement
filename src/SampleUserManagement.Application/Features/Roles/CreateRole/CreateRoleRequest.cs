using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Roles.CreateRole
{
    public record CreateRoleRequest(string Name) : IRequest<CreateRoleResponse>;
}
