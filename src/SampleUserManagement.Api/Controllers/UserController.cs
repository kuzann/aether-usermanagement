using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleUserManagement.Application.Features.Users.CreateUser;
using SampleUserManagement.Application.Features.Users.DeleteUser;
using SampleUserManagement.Application.Features.Users.FilterUser;
using SampleUserManagement.Application.Features.Users.GetUser;
using SampleUserManagement.Application.Features.Users.UpdateUser;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Api.Controllers
{
    /// <summary>
    /// API to handle APIs related to user features
    /// </summary>
    [Route("v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// API to search user by specified filter
        /// </summary>
        /// <param name="request">Filter user request</param>
        /// <param name="cancellationToken">token buat cancel</param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> Filter([FromQuery] FilterUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// API to get user by Id
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUserRequest(id), cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// API to create user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// API to update data user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request with { Id = id }, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// API to delete user by Id
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteUserRequest(id), cancellationToken);
            return Ok(response);
        }
    }
}
