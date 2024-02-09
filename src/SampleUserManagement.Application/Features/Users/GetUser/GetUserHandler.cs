﻿using AutoMapper;
using MediatR;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleUserManagement.Application.Features.Users.GetUser
{
	public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public GetUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<User>();
            _mapper = mapper;
        }

        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.Get(request.Id, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return _mapper.Map<GetUserResponse>(user);
        }
    }
}
