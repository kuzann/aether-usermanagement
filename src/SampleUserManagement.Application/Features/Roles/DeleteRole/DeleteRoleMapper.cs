using AutoMapper;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Roles.DeleteRole
{
	public class DeleteRoleMapper : Profile
	{
        public DeleteRoleMapper()
        {
            CreateMap<Role, DeleteRoleResponse>();
        }
    }
}
