using MediatR.Pipeline;
using SampleUserManagement.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Common.Exceptions
{
    public class GlobalExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TResponse : ErrorResponse, new()
        where TException : Exception

    {
        public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            var response = new TResponse()
            {
                Status = "status",
                Code = "code",
                Message = "message",
                Trace = "trace"
            };

            state.SetHandled(response);

            return Task.CompletedTask;
        }
    }
}
