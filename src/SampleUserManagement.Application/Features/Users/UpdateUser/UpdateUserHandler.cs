using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Application.Common.Extensions;
using SampleUserManagement.Application.Common.Interfaces;
using SampleUserManagement.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Users.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly IRepository<User> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<User>();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(request.Id, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.Email = request.Email;
            user.Password = request.Password;
            user.FullName = request.FullName;
            user.DateOfBirth = request.DateOfBirth?.ToDate(Constants.DATE_FORMAT);
            _repository.Update(user);
            await _unitOfWork.Commit();

            return _mapper.Map<UpdateUserResponse>(user);
        }
    }
}
