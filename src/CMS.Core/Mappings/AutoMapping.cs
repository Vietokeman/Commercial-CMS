﻿using AutoMapper;
using CMS.Core.Domain.Content;
using CMS.Core.Domain.Identity;
using CMS.Core.Models.Content;
using CMS.Core.System;

namespace CMS.Core.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Post, PostInListDto>();
            CreateMap<Post, PostDto>();
            CreateMap<CreateUpdatePostRequest, Post>();
            CreateMap<AppRole, RoleDto>();
        }
    }
}
