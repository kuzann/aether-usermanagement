using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common.Interfaces;
using SampleUserManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Users.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
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

        public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(request.Id, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            _repository.Delete(user);
            await _unitOfWork.Commit();

            return _mapper.Map<DeleteUserResponse>(user);
        }
    }
}
