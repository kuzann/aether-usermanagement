using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SampleUserManagement.Application.Common.Extensions;
using SampleUserManagement.Application.Common.Interfaces;
using SampleUserManagement.Application.Common.Responses;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Users.FilterUser
{
    public class FilterUserHandler : IRequestHandler<FilterUserRequest, PaginatedList<FilterUserResponse>>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        private const int DEFAULT_PAGE = 1;
        private const int DEFAULT_PAGE_SIZE = 10;

        public FilterUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _repository = unitOfWork.GetRepository<User>();
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<PaginatedList<FilterUserResponse>> Handle(FilterUserRequest request, CancellationToken cancellationToken)
        {
            var filters = request.QueryCollection.GetFilters();

            var users = _repository.Filter();

            // Implement filter
            if (filters.Any())
            {
                users = users.ApplyFilter<User>(filters);
            }

            // Implement sorting
            string? sort = request.QueryCollection["sort"];
            if (!string.IsNullOrEmpty(sort))
            {
                bool.TryParse(request.QueryCollection["sortasc"], out bool sortAsc);
                users = users.ApplySorting(sort, sortAsc);
            }

            // Implement pagination
            int limit = request.QueryCollection.GetIntValueFromQuery("limit", DEFAULT_PAGE_SIZE);
            int page = request.QueryCollection.GetIntValueFromQuery("page", DEFAULT_PAGE);
            int totalData = await _repository.CountAsync(users, cancellationToken);
            int totalPage = FilterExtensions.GetPageTotal(totalData, limit);
            users = users.ApplyPagination(page, limit);

            var data = _mapper.Map<List<FilterUserResponse>>(await _repository.ExecuteAsync(users, cancellationToken));

            var meta = new Meta(data.Count, totalData, page, totalPage, limit);

			return new PaginatedList<FilterUserResponse>(data, meta, new Links());
        }
    }
}
