using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Users.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
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

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            _repository.Create(user);
            await _unitOfWork.Commit();
            return _mapper.Map<CreateUserResponse>(user);
        }
    }
}
