using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common.Interfaces;
using SampleUserManagement.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Roles.GetRole
{
    public class GetRoleHandler : IRequestHandler<GetRoleRequest, GetRoleResponse>
    {
        private readonly IRepository<Role> _repository;
        private readonly IMapper _mapper;

        public GetRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<Role>();
            _mapper = mapper;
        }

        public async Task<GetRoleResponse> Handle(GetRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await _repository.Get(request.Id, cancellationToken);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            return _mapper.Map<GetRoleResponse>(role);
        }
    }
}
