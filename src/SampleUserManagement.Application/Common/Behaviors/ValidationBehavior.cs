using FluentValidation;
using MediatR;
using ValidationException = SampleUserManagement.Application.Common.Exceptions.ValidationException;

namespace SampleUserManagement.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var errors = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors);

				if (errors.Any())
                {
                    string message = errors.First().ErrorMessage;
                    throw new ValidationException(errors, message);
                }
            }

            return await next();
        }
    }
}
