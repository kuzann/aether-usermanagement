using AutoMapper;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Users.UpdateUser
{
	public class UpdateUserMapper : Profile
	{
        public UpdateUserMapper()
        {
            CreateMap<User, UserResponse>();
        }
    }
}
