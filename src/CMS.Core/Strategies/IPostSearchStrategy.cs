using CMS.Core.Models.Content;

namespace CMS.Core.Strategies
{
    public interface IPostSearchStrategy
    {
        Task<List<PostInListDto>> SearchAsync(string keyword);

    }
}
