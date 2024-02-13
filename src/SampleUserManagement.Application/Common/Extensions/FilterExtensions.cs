using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SampleUserManagement.Application.Common.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SampleUserManagement.Application.Common.Extensions
{
    public static class FilterExtensions
    {

        #region Filter

        public static IEnumerable<FilterModel> GetFilters(this IQueryCollection queryCollection)
        {
            List<FilterModel> filters = [];

            foreach (var query in queryCollection)
            {
                if (query.Key != null && query.Key.StartsWith("filter"))
                {
                    var matches = Regex.Matches(query.Key, "\\[.*?\\]");
                    var field = RemoveSquareBracket(matches[0].Value);
                    var op = RemoveSquareBracket(matches[1].Value).ToLower();
                    var value = query.Value.ToString();
                    filters.Add(new FilterModel(field, op, value));
                }
            }

            return filters;
        }

        private static string RemoveSquareBracket(string input)
        {
            return input.Replace("[", "").Replace("]", "");
        }

        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, IEnumerable<FilterModel> filters)
        {
            foreach (var filter in filters)
            {
                var test = ToLambda<T>(filter.Field);

                var test2 = query.AsQueryable().OrderBy(test);

                switch (filter.Operator)
                {
                    case "eq":
                        query = query.Where($"{filter.Field} == @0", filter.Value);
                        break;

                    case "ne":
                        query = query.Where($"{filter.Field} != @0", filter.Value);
                        break;

                    case "gt":
                        query = query.Where($"{filter.Field} > @0", filter.Value);
                        break;

                    case "gte":
                        query = query.Where($"{filter.Field} >= @0", filter.Value);
                        break;

                    case "lt":
                        query = query.Where($"{filter.Field} < @0", filter.Value);
                        break;

                    case "lte":
                        query = query.Where($"{filter.Field} <= @0", filter.Value);
                        break;

                    case "like":
                        //query = query.Where(e => EF.Functions.Like(e.Property, ));
                        query = query.Where(e => EF.Functions.Like(filter.Value, $"%{filter.Value}%"));
                        break;

                    case "nlike":
                        string propertyName = filter.Field;
                        object value = filter.Value;
                        ParameterExpression parameter = Expression.Parameter(typeof(T));
                        MemberExpression member = Expression.Property(parameter, propertyName);
                        var memberTypeConverter = TypeDescriptor.GetConverter(member.Type);
                        var constant = Expression.Constant(value == null ? null : memberTypeConverter.ConvertFrom(value.ToString()!), member.Type);

                        var body = Expression.Call(
                            typeof(DbFunctionsExtensions),
                            nameof(DbFunctionsExtensions.Like),
                            Type.EmptyTypes,
                            Expression.Property(null, typeof(EF), nameof(EF.Functions)),
                            member,
                            constant
                        );
                        var expr = Expression.Not(body);
                        query = query.Where(Expression.Lambda<Func<T, bool>>(expr, parameter));
                        break;

                    case "in":
                        break;

                    case "nin":
                        break;

                    default:
                        break;
                }
            }

            return query;
        }
        #endregion

        #region Sorting

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string sortBy, bool? sortAsc)
        {
            var propertyInfo = typeof(T).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (!string.IsNullOrEmpty(sortBy) && propertyInfo != null)
            {
                var expression = ToLambda<T>(sortBy);
                switch (sortAsc)
                {
                    case true:
                        query = query.OrderBy(expression);
                        break;
                    case false:
                        query = query.OrderByDescending(expression);
                        break;
                }
            }
            return query;
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }

        #endregion

        #region Pagination

        public static int GetIntValueFromQuery(this IQueryCollection queryCollection, string key, int defaultValue = 0)
        {
            if (int.TryParse(queryCollection[key], out var value))
            {
                return value;
            };
            return defaultValue;
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static int GetPageTotal(int totalData, int limit)
        {
            if (totalData % limit == 0)
            {
                return totalData / limit;
            }
            return (totalData / limit) + 1;
        }

        #endregion


        public static Expression<Func<T, object>> GetSortPropertyExpression<T>(string propertyName)
		{
			ParameterExpression parameter = Expression.Parameter(typeof(T));
			MemberExpression property = Expression.Property(parameter, propertyName);
			UnaryExpression propAsObject = Expression.Convert(property, typeof(object));

			return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
		}

		public static Expression<Func<T, object>> GetSortPropertyExpression<T>(string propertyName, string childEntityName)
		{
			ParameterExpression parameter = Expression.Parameter(typeof(T));
			MemberExpression childProperty = Expression.Property(parameter, childEntityName);
			MemberExpression property = Expression.Property(childProperty, propertyName);
			Expression conversion = Expression.Convert(property, typeof(object));
			UnaryExpression propAsObject = Expression.Convert(conversion, typeof(object));

			return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
		}
	}
}
