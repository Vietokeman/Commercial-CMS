using AutoMapper;
using CMS.Core.Domain.Content;
using CMS.Core.Domain.Identity;
using CMS.Core.Models.Content;
using CMS.Core.Models.System;
using CMS.Core.Services;

namespace CMS.Core.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Post, PostInListDto>()
                .ForMember(dest => dest.EstimatedReadingTimeMinutes, 
                    opt => opt.MapFrom(src => ReadingTimeCalculator.CalculateReadingTime(src.Content)));
            
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.EstimatedReadingTimeMinutes, 
                    opt => opt.MapFrom(src => ReadingTimeCalculator.CalculateReadingTime(src.Content)));
            
            CreateMap<CreateUpdatePostRequest, Post>();
            CreateMap<AppRole, RoleDto>();
            CreateMap<AppUser, UserDto>();
            CreateMap<CreateUserRequest, AppUser>();
            CreateMap<UpdateUserRequest, AppUser>();
            CreateMap<PostCategory, PostCategoryDto>();
            CreateMap<CreateUpdatePostCategoryRequest, PostCategory>();
            CreateMap<Series, SeriesInListDto>();
            CreateMap<Series, SeriesDto>();
            CreateMap<CreateUpdateSeriesRequest, Series>();
            CreateMap<PostActivityLog, PostActivityLogDto>();
        }
    }
}
