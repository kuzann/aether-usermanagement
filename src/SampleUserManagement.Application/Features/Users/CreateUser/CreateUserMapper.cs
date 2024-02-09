using AutoMapper;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Application.Extensions;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Users.CreateUser
{
    public class CreateUserMapper : Profile
    {
        public CreateUserMapper() 
        {
            CreateMap<CreateUserRequest, User>()
                .ForMember(user => user.DateOfBirth, config => config.MapFrom(source => source.DateOfBirth.ToDate(Constants.DATE_FORMAT)));
			CreateMap<User, CreateUserResponse>()
                .ForMember(response => response.DateOfBirth, config => config.MapFrom(source => source.DateOfBirth.ToString(Constants.DATE_FORMAT)));
        }
    }
}
