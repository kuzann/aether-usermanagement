using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Common.Responses
{
	public record PaginatedList<T>
	{
        public Meta Meta { get; set; } = null!;
        public IEnumerable<T> Data { get; set; }
        public Links Links { get; set; } = null!;

        public PaginatedList()
        {
            Data = new List<T>();
        }

        public PaginatedList(List<T> data)
        {
            Data = data;
            Meta = new Meta();
            Links = new Links();
        }
    }

    public record Meta
    {
        public int Count { get; init; }
        public int Total { get; init; }
        public int Page { get; init; }
        [JsonPropertyName("page_total")]
        public int PageTotal { get; init; }
        public int Limit { get; init; }

        public Meta(int count, int total, int page, int pageTotal, int limit)
        {
            Count = count;
            Total = total;
            Page = page;
            PageTotal = pageTotal;
            Limit = limit;
        }

        public Meta()
        {
            
        }
    }

    public record Links
    {
        public string? Next { get; init; }
        public string? Prev { get; init; }
        public string? First { get; init; }
        public string? Last { get; init; }

        public Links(string next, string prev, string first, string last)
        {
            Next = next;
            Prev = prev;
            First = first;
            Last = last;
        }

        public Links()
        {
            
        }
    };
}
