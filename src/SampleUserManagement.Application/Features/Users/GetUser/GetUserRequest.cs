using MediatR;
using System;

namespace SampleUserManagement.Application.Features.Users.GetUser
{
    public record GetUserRequest(Guid Id) : IRequest<GetUserResponse>;
}
