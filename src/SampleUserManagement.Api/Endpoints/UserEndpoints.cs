﻿using MediatR;
using SampleUserManagement.Application.Common.Responses;
using SampleUserManagement.Application.Features.Users;
using SampleUserManagement.Application.Features.Users.CreateUser;
using SampleUserManagement.Application.Features.Users.DeleteUser;
using SampleUserManagement.Application.Features.Users.FilterUser;
using SampleUserManagement.Application.Features.Users.GetUser;
using SampleUserManagement.Application.Features.Users.UpdateUser;

namespace SampleUserManagement.Api.Endpoints
{
    /// <summary>
    /// Endpoint to handle APIs related to user features
    /// </summary>
    public static class UserEndpoints
	{
		/// <summary>
		/// Method to register user minimal API
		/// </summary>
		/// <param name="builder"></param>
		public static void MapUserEndpoints(this IEndpointRouteBuilder builder)
		{
			var group = builder.MapGroup("v1/user").WithTags("User");

			group.MapGet("/", GetUser);
			group.MapGet("{id}", GetUserById);
			group.MapPost("/", CreateUser);
			group.MapPut("{id}", UpdateUser);
			group.MapDelete("{id}", DeleteUser);
		}

		#region Endpoint

		/// <summary>
		/// API to search user by specified filter
		/// </summary>
		/// <param name="mediator"></param>
		/// <param name="httpContext"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		private static async Task<PaginatedList<UserResponse>> GetUser(IMediator mediator, HttpContext httpContext, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new FilterUserRequest(httpContext.Request.Query), cancellationToken);
            return result;
        }

		/// <summary>
		/// API to get user by Id
		/// </summary>
		/// <param name="mediator"></param>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		private static async Task<BaseResponse<UserResponse>> GetUserById(IMediator mediator, Guid id, CancellationToken cancellationToken) 
		{
			var result = await mediator.Send(new GetUserRequest(id), cancellationToken);
			return result;
		}

        /// <summary>
        /// API to create new user
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async Task<BaseResponse<UserResponse>> CreateUser(IMediator mediator, CreateUserRequest request, CancellationToken cancellationToken)
		{
			var result = await mediator.Send(request, cancellationToken);
			return result;
		}

		/// <summary>
		/// API to update existing user
		/// </summary>
		/// <param name="mediator"></param>
		/// <param name="id"></param>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		private static async Task<BaseResponse<UserResponse>> UpdateUser(IMediator mediator, Guid id, UpdateUserRequest request, CancellationToken cancellationToken)
		{
			var result = await mediator.Send(request with { Id = id }, cancellationToken);
			return result;
		}

		/// <summary>
		/// API to delete existing user
		/// </summary>
		/// <param name="mediator"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		private static async Task<BaseResponse<UserResponse>> DeleteUser(IMediator mediator, Guid id)
		{
			var result = await mediator.Send(new DeleteUserRequest(id));
			return result;
		}
		#endregion

	}
}