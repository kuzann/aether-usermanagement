using MediatR.Pipeline;
using SampleUserManagement.Application.Common.Responses;

namespace SampleUserManagement.Application.Common.Exceptions
{
	public class GlobalExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
		where TResponse : BaseResponse, new()
		where TException : Exception
	{
		public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
		{
			switch (exception)
			{
				case NotImplementedException:
					state.SetHandled(new ErrorResponse("Internal Server Error", "500", exception.Message, null) as TResponse);
					break;
				default:
					break;
			}

			return Task.CompletedTask;
		}
	}
}
