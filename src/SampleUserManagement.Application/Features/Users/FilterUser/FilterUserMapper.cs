using AutoMapper;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Application.Common.Extensions;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Users.FilterUser
{
    public class FilterUserMapper : Profile
	{
        public FilterUserMapper()
        {
            CreateMap<User, UserResponse>()
                .ForMember(response => response.DateOfBirth, config => config.MapFrom(source => source.DateOfBirth.ToString(Constants.DATE_FORMAT)));
        }
    }
}
