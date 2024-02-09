using System;

namespace SampleUserManagement.Application.Features.Users.GetUser
{
	public record GetUserResponse()
	{
        public Guid Id { get; init; }
        public string Email { get; init; } = null!;
		public string? FullName { get; init; }
        public string? DateOfBirth { get; init; }
    }
}
