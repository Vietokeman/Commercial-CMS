using CMS.Core.Models.Content;
using CMS.Core.Repositories;
using CMS.Core.Strategies;

namespace CMS.Data.Strategies
{
    public class SearchBySlugStrategy : IPostSearchStrategy
    {
        private readonly IPostRepository _postRepository;

        public SearchBySlugStrategy(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostInListDto>> SearchAsync(string slug)
        {
            return await _postRepository.SearchPostsBySlugAsync(slug);
        }
    }
}
