using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SampleUserManagement.Application.Common.Filters;
using SampleUserManagement.Application.Common.Interfaces;
using SampleUserManagement.Application.Common.Responses;
using SampleUserManagement.Domain.Entities;
using System.Linq.Expressions;

namespace SampleUserManagement.Application.Features.Users.FilterUser
{
	public record FilterUserRequest() : IRequest<PaginatedList<UserResponse>>;

	public class FilterUserHandler : IRequestHandler<FilterUserRequest, PaginatedList<UserResponse>>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;

        private const int DEFAULT_PAGE = 1;
        private const int DEFAULT_PAGE_SIZE = 10;

        public FilterUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = unitOfWork.GetRepository<User>();
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<PaginatedList<UserResponse>> Handle(FilterUserRequest request, CancellationToken cancellationToken)
        {
            IQueryCollection queryCollection = _httpContext.Request.Query;
			var filters = queryCollection.GetFilters();

            var users = _repository.Filter();

            // Implement filter
            if (filters.Any())
            {
                users = users.ApplyFilter(filters);
            }

            //var parameter = Expression.Parameter(typeof(User));
            //var property = Expression.Property(parameter, "FullName");
            //var propAsObject = Expression.Convert(property, typeof(object));

            //var sortExpr = Expression.Lambda<Func<User, object>>(propAsObject, parameter);

            //Func<User, bool> filterFunc = user => user.FullName.Equals("Marion Bosco");
            //Expression<Func<User, bool>> filterExprConvert = user => filterFunc(user);
            //var compiler = filterExprConvert.Compile();
            //users = users.Where(user => filterFunc.Invoke(user));

            //Expression<Func<User, bool>> filterExprDirect = user => user.FullName.Contains("Ida");
            //users = users.Where(filterExprDirect);

            // Implement sorting
            string? sort = queryCollection["sort"];
            if (!string.IsNullOrEmpty(sort))
            {
                bool.TryParse(queryCollection["sortasc"], out bool sortAsc);
                users = users.ApplySorting(sort, sortAsc);
            }

            // Implement pagination
            int limit = queryCollection.GetIntValueFromQuery("limit", DEFAULT_PAGE_SIZE);
            int page = queryCollection.GetIntValueFromQuery("page", DEFAULT_PAGE);
            int totalData = await _repository.CountAsync(users, cancellationToken);
            int totalPage = FilterExtensions.GetPageTotal(totalData, limit);
            users = users.ApplyPagination(page, limit);

            var data = _mapper.Map<List<UserResponse>>(await _repository.ExecuteAsync(users, cancellationToken));

            var meta = new Meta(data.Count, totalData, page, totalPage, limit);


            List<string> queryStrings = [];
			foreach (var query in queryCollection)
            {
                if (query.Key != "page")
                {
					queryStrings.Add($"{query.Key}={query.Value}");
                }
            }

			string queryPath = _httpContext.Request.Path + "?" + string.Join("&", queryStrings);
			string next = page >= totalPage ? "" : queryPath + GeneratePageQueryString(page + 1, queryStrings.Count);
			string previous = page <= 1 ? "" : queryPath + GeneratePageQueryString(page >= totalPage ? totalPage - 1 : page - 1, queryStrings.Count);
            string first = queryPath + GeneratePageQueryString(1, queryStrings.Count);
            string last = queryPath + GeneratePageQueryString(totalPage, queryStrings.Count);
            var links = new Links(next, previous, first, last);

			return new PaginatedList<UserResponse>(data, meta, links);
        }

        private string GeneratePageQueryString(int page, int queryStringCount)
        {
            return (queryStringCount > 0 ? "&" : "") + $"page={page}";
        }
    }
}
