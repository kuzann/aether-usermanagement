using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Common.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public string Status { get; set; } = null!;
        public string Code { get; set; } = null!;
		public string Message { get; set; } = null!;
		public string? Trace { get; set; }

        public ErrorResponse()
        {
            
        }

        public ErrorResponse(string status, string code, string message, string? trace)
        {
            Status = status;
            Code = code;
            Message = message;
            Trace = trace;
        }
    }
}
