using AutoMapper;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Roles.FilterRole
{
	internal class FilterRoleMapper : Profile
	{
        public FilterRoleMapper()
        {
            CreateMap<Role, FilterRoleResponse>();
        }
    }
}
