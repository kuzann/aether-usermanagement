using AutoMapper;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Users.DeleteUser
{
	public class DeleteUserMapper : Profile
	{
        public DeleteUserMapper()
        {
            CreateMap<User, UserResponse>();
        }
    }
}
