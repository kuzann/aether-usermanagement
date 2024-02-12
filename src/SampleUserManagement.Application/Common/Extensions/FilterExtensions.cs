using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SampleUserManagement.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Common.Extensions
{
    public static class FilterExtensions
    {
        public static bool IsPropertyExists(this object obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            return propertyInfo != null;
        }

        public static IEnumerable<FilterModel> ToFilter(this IQueryCollection queryCollection)
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
    }
}
