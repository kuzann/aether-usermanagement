﻿using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Application.Common.Exceptions;
using SampleUserManagement.Application.Common.Extensions;
using SampleUserManagement.Application.Common.Interfaces;
using SampleUserManagement.Application.Common.Responses;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Users.UpdateUser
{
	public record UpdateUserRequest : IRequest<BaseResponse>
	{
		public Guid Id { get; init; }
		public string Email { get; init; } = null!;
		public string Password { get; init; } = null!;
		public string? FullName { get; init; }
		public string? DateOfBirth { get; init; }
	}

	public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, BaseResponse>
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

        public async Task<BaseResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            user.Email = request.Email;
            user.Password = request.Password;
            user.FullName = request.FullName;
            user.DateOfBirth = request.DateOfBirth?.ToDate(Constants.DATE_FORMAT);
            _repository.Update(user);
            await _unitOfWork.Commit();

            return new BaseResponse(_mapper.Map<UserResponse>(user));
        }
    }
}
