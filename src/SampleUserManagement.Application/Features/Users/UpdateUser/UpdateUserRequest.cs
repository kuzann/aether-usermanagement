using MediatR;
using System;

namespace SampleUserManagement.Application.Features.Users.UpdateUser
{
	public record UpdateUserRequest : IRequest<UpdateUserResponse>
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = null!;
		public string Password { get; init; } = null!;
		public string? FullName { get; init; }
        public string? DateOfBirth { get; init; }
    }
}
