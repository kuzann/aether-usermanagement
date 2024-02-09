using AutoMapper;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Roles.GetRole
{
	public class GetUserMapper : Profile
    {
        public GetUserMapper()
        {
            CreateMap<Role, GetRoleResponse>();
        }
    }
}
