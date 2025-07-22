using CMS.Core.Models.Content;
using CMS.Core.Repositories;
using CMS.Core.Strategies;

namespace CMS.Data.Strategies
{
    public class SearchByCategoryStrategy : IPostSearchStrategy
    {
        private readonly IPostRepository _postRepository;

        public SearchByCategoryStrategy(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostInListDto>> SearchAsync(string categorySlug)
        {
            return await _postRepository.SearchPostsByCategorySlugAsync(categorySlug);
        }
    }

}
