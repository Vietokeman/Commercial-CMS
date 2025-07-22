using CMS.Core.Models.Content;
using CMS.Core.Strategies;

namespace CMS.Data.Strategies
{
    public class SearchContext
    {
        private IPostSearchStrategy _strategy;

        public SearchContext(IPostSearchStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IPostSearchStrategy strategy)
        {
            _strategy = strategy;
        }

        public Task<List<PostInListDto>> SearchAsync(string keyword)
        {
            return _strategy.SearchAsync(keyword);
        }
    }
}
