﻿using AutoMapper;
using SampleUserManagement.Application.Common;
using SampleUserManagement.Application.Extensions;
using SampleUserManagement.Domain.Entities;

namespace SampleUserManagement.Application.Features.Users.GetUser
{
	public class GetUserMapper : Profile
    {
        public GetUserMapper()
        {
            CreateMap<User, GetUserResponse>()
                .ForMember(response => response.DateOfBirth, config => config.MapFrom(source => source.DateOfBirth.ToString(Constants.DATE_FORMAT)));
        }
    }
}
