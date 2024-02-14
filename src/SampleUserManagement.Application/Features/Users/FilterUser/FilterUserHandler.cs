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
	public record FilterUserRequest(IQueryCollection QueryCollection) : IRequest<PaginatedList<UserResponse>>;

	public class FilterUserHandler : IRequestHandler<FilterUserRequest, PaginatedList<UserResponse>>
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

        public async Task<PaginatedList<UserResponse>> Handle(FilterUserRequest request, CancellationToken cancellationToken)
        {
            var filters = request.QueryCollection.GetFilters();

            var users = _repository.Filter();

            // Implement filter
            if (filters.Any())
            {
                //users = users.ApplyFilter<User>(filters);
            }

            var parameter = Expression.Parameter(typeof(User));
            var property = Expression.Property(parameter, "FullName");
            var propAsObject = Expression.Convert(property, typeof(object));

            var sortExpr = Expression.Lambda<Func<User, object>>(propAsObject, parameter);

            Func<User, bool> filterFunc = user => user.FullName.Equals("Marion Bosco");
            Expression<Func<User, bool>> filterExprConvert = user => filterFunc(user);
			//var compiler = filterExprConvert.Compile();
			users = users.Where(user => filterFunc.Invoke(user));

			Expression<Func<User, bool>> filterExprDirect = user => user.FullName.Contains("Ida");
            //users = users.Where(filterExprDirect);

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

            var data = _mapper.Map<List<UserResponse>>(await _repository.ExecuteAsync(users, cancellationToken));

            var meta = new Meta(data.Count, totalData, page, totalPage, limit);

			return new PaginatedList<UserResponse>(data, meta, new Links());
        }
    }
}
