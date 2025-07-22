using CMS.Core.Models.Content;

namespace CMS.WebApp.Models
{
    public class HomeViewModel
    {
        public SearchInputModel SearchInput { get; set; } = new();

        public List<PostInListDto> LatestPosts { get; set; }

    }
}
