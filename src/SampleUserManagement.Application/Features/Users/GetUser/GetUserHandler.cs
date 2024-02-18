using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common.Exceptions;
using SampleUserManagement.Application.Common.Interfaces;
using SampleUserManagement.Application.Common.Responses;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Users.GetUser
{
	public record GetUserRequest(Guid Id) : IRequest<BaseResponse>;

	public class GetUserHandler : IRequestHandler<GetUserRequest, BaseResponse>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public GetUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<User>();
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return new BaseResponse(_mapper.Map<UserResponse>(user));
        }
    }
}
