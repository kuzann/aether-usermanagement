using FluentValidation;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Application.Extensions;

namespace SampleUserManagement.Application.Features.Users.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleLevelCascadeMode = ClassLevelCascadeMode;

            RuleFor(req => req.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(req => req.Password)
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("Password must be atleast 8 characters");

            string dateFormat = Constants.DATE_FORMAT;
            RuleFor(req => req.DateOfBirth)
                .Must(str => str.ToDate(dateFormat) != null || string.IsNullOrWhiteSpace(str))
                .WithMessage($"Invalid date/time. Format should follow this format {dateFormat}");
        }
    }
}
