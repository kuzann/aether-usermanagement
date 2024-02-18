using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleUserManagement.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures, string message) : base(message)
        {
            Errors = GetErrors(failures);

		}

        public ValidationException(IEnumerable<ValidationFailure> failures, string message, Exception inner) : base(message, inner)
		{
			Errors = GetErrors(failures);
		}

        public IDictionary<string, string[]> Errors { get; }

        private Dictionary<string, string[]> GetErrors(IEnumerable<ValidationFailure> failures)
        {
            return failures
				.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
				.ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
		}
	}
}
