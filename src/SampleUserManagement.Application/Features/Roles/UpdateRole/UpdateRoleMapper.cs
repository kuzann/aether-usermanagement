using AutoMapper;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Roles.UpdateRole
{
	public class UpdateRoleMapper : Profile
	{
        public UpdateRoleMapper()
        {
            CreateMap<Role, UpdateRoleResponse>();
        }
    }
}
