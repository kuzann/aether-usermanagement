using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SampleUserManagement.Application.Common.Models;
using System.ComponentModel;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SampleUserManagement.Application.Common.Filters
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
                switch (filter.Operator)
                {
                    case "eq":
                        query = query.Where($"{filter.Field} eq @0", filter.Value);
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
                        query = query.Where($"{filter.Field}.Contains(@0)", filter.Value);
                        break;

                    case "nlike":
                        query = query.Where($"!{filter.Field}.Contains(@0)", filter.Value);
                        //var nlikeExprCall = Expression.Not(GetLikeExpressionCall<T>(filter.Field, filter.Value));
                        //var nlikeExpr = Expression.Lambda<Func<T, bool>>(nlikeExprCall, Expression.Parameter(typeof(T)));
                        //                  query = query.Where(nlikeExpr);
                        break;

                    case "in":
                        List<string> valuesIn = filter.Value.Split(',').ToList();
                        query = query.Where($"{filter.Field} in @0", valuesIn);
                        break;

                    case "nin":
                        foreach (var value in filter.Value.Split(','))
                        {
                            query = query.Where($"{filter.Field} != @0", value);
                        }
                        break;

                    default:
                        break;
                }
            }

            return query;
        }

        private static MethodCallExpression GetLikeExpressionCall<T>(string propertyName, object value)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            MemberExpression member = Expression.Property(parameter, propertyName);
            var memberTypeConverter = TypeDescriptor.GetConverter(member.Type);
            var constant = Expression.Constant(value == null ? null : memberTypeConverter.ConvertFrom(value.ToString()!), member.Type);

            return Expression.Call(
                typeof(DbFunctionsExtensions),
                nameof(DbFunctionsExtensions.Like),
                Type.EmptyTypes,
                Expression.Property(null, typeof(EF), nameof(EF.Functions)),
                member,
                constant
            );
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
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            MemberExpression property = Expression.Property(parameter, propertyName);
            UnaryExpression propAsObject = Expression.Convert(property, typeof(object));

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
            return totalData / limit + 1;
        }

        #endregion
    }
}
