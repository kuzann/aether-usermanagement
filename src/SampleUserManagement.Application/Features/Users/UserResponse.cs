using System;

namespace SampleUserManagement.Application.Features.Users
{
    public record UserResponse()
    {
        public Guid Id { get; init; }
        public string Email { get; init; } = null!;
        public string? FullName { get; init; }
        public string? DateOfBirth { get; init; }
    }
}
