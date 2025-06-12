using CMS.Core.Models.Content;

namespace CMS.WebApp.Models
{
    public class PostDetailViewModel
    {
        public PostDto Post { get; set; }
        public PostCategoryDto Category { get; set; }
    }
}
