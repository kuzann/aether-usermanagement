using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using SampleUserManagement.Application.Common.Responses;

namespace SampleUserManagement.Application.Common.Exceptions
{
	public class GlobalExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
		where TRequest : notnull
		where TResponse : BaseResponse, new()
		where TException : Exception
	{
		public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
		{
			var errorResponse = new ErrorResponse()
			{
				Message = exception.Message
			};
			
			// put trace if environment is local or development...

			switch (exception)
			{
				case ValidationException:
					SetErrorResponse(errorResponse, "Invalid request", StatusCodes.Status400BadRequest);
					break;
				case NotAuthenticatedException:
					SetErrorResponse(errorResponse, "Unauthenticated access", StatusCodes.Status401Unauthorized);
					break;
				case NotAuthorizedException:
					SetErrorResponse(errorResponse, "User is not granted to access this resource", StatusCodes.Status403Forbidden);
					break;
				case NotFoundException:
					SetErrorResponse(errorResponse, "Resource is not available", StatusCodes.Status404NotFound);
					break;
				case UnprocessableDataException:
					SetErrorResponse(errorResponse, "Resource cannot be updated", StatusCodes.Status422UnprocessableEntity);
					break;
				default:
					SetErrorResponse(errorResponse, "Internal Server Error", StatusCodes.Status500InternalServerError);
					break;
			}
			state.SetHandled((TResponse)(dynamic)errorResponse);

			return Task.CompletedTask;
		}

		private void SetErrorResponse(ErrorResponse response, string status, int code)
		{
			response.Status = status;
			response.Code = code.ToString();
		}
	}
}
