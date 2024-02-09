using MediatR;
using System.Collections.Generic;

namespace SampleUserManagement.Application.Features.Users.FilterUser
{
    public record FilterUserRequest(string? Email, string? FullName) : IRequest<List<FilterUserResponse>>;
}
