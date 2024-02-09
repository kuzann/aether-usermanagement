using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Roles.UpdateRole
{
	public class UpdateRoleHandler : IRequestHandler<UpdateRoleRequest, UpdateRoleResponse>
    {
        private readonly IRepository<Role> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<Role>();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateRoleResponse> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await _repository.Get(request.Id, cancellationToken);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            role.Name = request.Name;
            _repository.Update(role);
            await _unitOfWork.Commit();

            return _mapper.Map<UpdateRoleResponse>(role);
        }
    }
}
