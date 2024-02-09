using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Roles.DeleteRole
{
	public class DeleteRoleHandler : IRequestHandler<DeleteRoleRequest, DeleteRoleResponse>
    {
        private readonly IRepository<Role> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<Role>();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DeleteRoleResponse> Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await _repository.Get(request.Id, cancellationToken);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            _repository.Delete(role);
            await _unitOfWork.Commit();

            return _mapper.Map<DeleteRoleResponse>(role);
        }
    }
}
