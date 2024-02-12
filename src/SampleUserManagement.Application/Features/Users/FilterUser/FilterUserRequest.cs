using MediatR;
using Microsoft.AspNetCore.Http;
using SampleUserManagement.Application.Common.Responses;
using System.Collections.Generic;

namespace SampleUserManagement.Application.Features.Users.FilterUser
{
    public record FilterUserRequest(IQueryCollection QueryCollection) : IRequest<PaginatedList<FilterUserResponse>>;
}
