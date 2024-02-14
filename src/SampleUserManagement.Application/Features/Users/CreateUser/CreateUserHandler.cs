using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common.Interfaces;
using SampleUserManagement.Application.Common.Responses;
using SampleUserManagement.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Users.CreateUser
{
	public record CreateUserRequest(string Email, string Password, string? FullName, string? DateOfBirth) : IRequest<BaseResponse<UserResponse>>;

	public class CreateUserHandler : IRequestHandler<CreateUserRequest, BaseResponse<UserResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _repository;

        public CreateUserHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<User>();
        }

        public async Task<BaseResponse<UserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            _repository.Create(user);
            await _unitOfWork.Commit();
            return new BaseResponse<UserResponse>(_mapper.Map<UserResponse>(user));
        }
    }
}
