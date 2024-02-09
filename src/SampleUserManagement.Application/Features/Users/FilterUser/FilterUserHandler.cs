using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Users.FilterUser
{
    public class FilterUserHandler : IRequestHandler<FilterUserRequest, List<FilterUserResponse>>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public FilterUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _repository = unitOfWork.GetRepository<User>();
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<List<FilterUserResponse>> Handle(FilterUserRequest request, CancellationToken cancellationToken)
        {
            var users = _repository.GetAll();
            if (!string.IsNullOrEmpty(request.Email))
            {
                users = users.Where(user => user.Email.Contains(request.Email));
            }
			if (!string.IsNullOrEmpty(request.FullName))
			{
				users = users.Where(user => !string.IsNullOrEmpty(user.FullName) && user.FullName.Contains(request.FullName));
			}
			return _mapper.Map<List<FilterUserResponse>>(await users.ToListAsync(cancellationToken));
        }
    }
}
