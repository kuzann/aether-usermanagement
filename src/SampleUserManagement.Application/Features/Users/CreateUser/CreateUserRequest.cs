using MediatR;
using System;
using System.Xml.Serialization;

namespace SampleUserManagement.Application.Features.Users.CreateUser
{
	public record CreateUserRequest(string Email, string Password, string? FullName, string? DateOfBirth) : IRequest<CreateUserResponse>;
}
