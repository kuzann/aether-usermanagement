using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Product.Delete
{
	public class DeleteProductHandler : IRequestHandler<ProductIdRequest, ProductResponse>
	{
		public async Task<ProductResponse> Handle(ProductIdRequest request, CancellationToken cancellationToken)
		{
			return new ProductResponse()
			{
				Name = "From delete"
			};
		}
	}
}
