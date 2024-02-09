using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Roles.CreateRole
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleRequest, CreateRoleResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Role> _repository;

        public CreateRoleHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<Role>();
        }

        public async Task<CreateRoleResponse> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<Role>(request);
            _repository.Create(role);
            await _unitOfWork.Commit();
            return _mapper.Map<CreateRoleResponse>(role);
        }
    }
}
