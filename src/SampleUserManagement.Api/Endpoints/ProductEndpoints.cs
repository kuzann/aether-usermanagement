using MediatR;
using SampleUserManagement.Application.Features.Product;

namespace SampleUserManagement.Api.Endpoints
{
	public static class ProductEndpoints
	{
		public static void MapProductEndpoints(this IEndpointRouteBuilder builder)
		{
			var group = builder.MapGroup("v1/product").WithTags("Role");

			group.MapGet("get/{id}", GetById);
			group.MapGet("delete/{id}", DeleteById);
		}

		private static async Task<ProductResponse> GetById(IMediator mediator, int id, CancellationToken cancellationToken)
		{
			var result = await mediator.Send(new ProductIdRequest(id));
			return result;
		}

		private static async Task<ProductResponse> DeleteById(IMediator mediator, int id, CancellationToken cancellationToken)
		{
			var result = await mediator.Send(new ProductIdRequest(id));
			return result;
		}
	}
}
