using AutoMapper;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Roles.CreateRole
{
    public class CreateRoleMapper : Profile
    {
        public CreateRoleMapper() 
        {
            CreateMap<CreateRoleRequest, Role>();
			CreateMap<Role, CreateRoleResponse>();
		}
    }
}
