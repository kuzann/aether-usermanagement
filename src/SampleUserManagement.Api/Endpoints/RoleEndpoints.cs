using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Forms;
using SampleUserManagement.Application.Features.Roles.CreateRole;
using SampleUserManagement.Application.Features.Roles.DeleteRole;
using SampleUserManagement.Application.Features.Roles.FilterRole;
using SampleUserManagement.Application.Features.Roles.GetRole;
using SampleUserManagement.Application.Features.Roles.UpdateRole;

namespace SampleUserManagement.Api.Endpoints
{
	/// <summary>
	/// Endpoint to handle APIs related to role features
	/// </summary>
	public static class RoleEndpoints
	{
		/// <summary>
		/// Method to handle role minimal API
		/// </summary>
		/// <param name="builder"></param>
		public static void MapRoleEndpoints(this IEndpointRouteBuilder builder)
		{
			var group = builder.MapGroup("v1/role").WithTags("Role");

			group.MapGet("/", GetRole);
			group.MapGet("{id}", GetRoleById);
			group.MapPost("/", CreateRole);
			group.MapPut("{id}", UpdateRole);
			group.MapDelete("{id}", DeleteRole);
		}

        #region Endpoint
        
		/// <summary>
        /// API to search role by specified filter
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<List<FilterRoleResponse>> GetRole(IMediator mediator, [AsParameters] FilterRoleRequest request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(request, cancellationToken);
            return result;
        }

		/// <summary>
		/// API to get role by Id
		/// </summary>
		/// <param name="mediator"></param>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		private static async Task<GetRoleResponse> GetRoleById(IMediator mediator, Guid id, CancellationToken cancellationToken) 
		{
			var result = await mediator.Send(new GetRoleRequest(id), cancellationToken);
			return result;
		}

        /// <summary>
        /// API to create role
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<CreateRoleResponse> CreateRole(IMediator mediator, CreateRoleRequest request, CancellationToken cancellationToken)
		{
			var result = await mediator.Send(request, cancellationToken);
			return result;
		}

		/// <summary>
		/// API to update role
		/// </summary>
		/// <param name="mediator"></param>
		/// <param name="id"></param>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		private static async Task<UpdateRoleResponse> UpdateRole(IMediator mediator, Guid id, UpdateRoleRequest request, CancellationToken cancellationToken)
		{
			var result = await mediator.Send(request with { Id = id }, cancellationToken);
			return result;
		}

		/// <summary>
		/// API to delete role
		/// </summary>
		/// <param name="mediator"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		private static async Task<DeleteRoleResponse> DeleteRole(IMediator mediator, Guid id)
		{
			var result = await mediator.Send(new DeleteRoleRequest(id));
			return result;
		}
		#endregion

	}
}
