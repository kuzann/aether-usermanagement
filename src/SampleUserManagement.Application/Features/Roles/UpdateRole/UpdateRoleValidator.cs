using FluentValidation;

namespace SampleUserManagement.Application.Features.Roles.UpdateRole
{
	public class UpdateRoleValidator : AbstractValidator<UpdateRoleRequest>
	{
		public UpdateRoleValidator()
		{
			RuleFor(req => req.Name)
				.NotEmpty();
		}
	}
}
