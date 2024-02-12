using MediatR;
using SampleUserManagement.Application.Common.Responses;
using System;

namespace SampleUserManagement.Application.Features.Users.GetUser
{
    public record GetUserRequest(Guid Id) : IRequest<BaseResponse<GetUserResponse>>;
}
