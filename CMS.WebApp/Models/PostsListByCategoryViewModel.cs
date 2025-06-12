using CMS.Core.Models;
using CMS.Core.Models.Content;

namespace CMS.WebApp.Models
{
    public class PostsListByCategoryViewModel
    {
        public PostCategoryDto? Category { get; set; }
        public PageResult<PostInListDto>? Posts { get; set; }
    }
}
