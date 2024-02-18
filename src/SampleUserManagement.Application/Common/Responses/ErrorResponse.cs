using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Common.Responses
{
    public record ErrorResponse : BaseResponse
    {
        public string Status { get; init; }
        public string Code { get; init; }
        public string Message { get; init; }
        public string? Trace { get; init; }

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
