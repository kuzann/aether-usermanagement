using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common.Exceptions;
using SampleUserManagement.Application.Common.Interfaces;
using SampleUserManagement.Application.Common.Responses;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Users.DeleteUser
{
	public record DeleteUserRequest(Guid Id) : IRequest<BaseResponse>;

	public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, BaseResponse>
    {
        private readonly IRepository<User> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<User>();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            _repository.Delete(user);
            await _unitOfWork.Commit();

            return new BaseResponse(_mapper.Map<UserResponse>(user));
        }
    }
}
