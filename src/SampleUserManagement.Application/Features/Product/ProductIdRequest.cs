using MediatR;

namespace SampleUserManagement.Application.Features.Product
{
    public record ProductIdRequest(int id) : IRequest<ProductResponse>;
}
