using AutoMapper;
using CMS.Core.Domain.Content;
using CMS.Core.Models.Content;

namespace CMS.Core.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Post, PostInListDto>();
            CreateMap<Post, PostDto>();
            CreateMap<CreateUpdatePostRequest, Post>();
        }
    }
}
