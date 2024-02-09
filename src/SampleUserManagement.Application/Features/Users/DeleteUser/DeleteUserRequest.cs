using MediatR;
using System;

namespace SampleUserManagement.Application.Features.Users.DeleteUser
{
    public record DeleteUserRequest(Guid Id) : IRequest<DeleteUserResponse>;
}
