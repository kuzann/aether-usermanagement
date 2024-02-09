using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Roles.FilterRole
{
    public class FilterRoleHandler : IRequestHandler<FilterRoleRequest, List<FilterRoleResponse>>
    {
        private readonly IRepository<Role> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public FilterRoleHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _repository = unitOfWork.GetRepository<Role>();
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<List<FilterRoleResponse>> Handle(FilterRoleRequest request, CancellationToken cancellationToken)
        {
            var roles = _repository.GetAll();
            if (!string.IsNullOrEmpty(request.Name))
            {
                roles = roles.Where(user => user.Name.Contains(request.Name));
            }
            return _mapper.Map<List<FilterRoleResponse>>(await roles.ToListAsync(cancellationToken));
        }
    }
}
