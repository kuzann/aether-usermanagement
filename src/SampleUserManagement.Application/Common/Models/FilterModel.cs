using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Common.Models
{
    public class FilterModel
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; } = default!;

        public FilterModel(string field, string op, string value)
        {
            Field = field;
            Operator = op;
            Value = value;
        }
    }
}
