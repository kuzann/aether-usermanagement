using FluentValidation;

namespace SampleUserManagement.Application.Features.Roles.CreateRole
{
	public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
	{
        public CreateRoleValidator()
        {
            RuleFor(req => req.Name)
                .NotEmpty();
        }
    }
}
