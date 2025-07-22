using CMS.Core.Models.Content;
using CMS.Core.SeedWorks;

namespace CMS.Data.Facades
{
    public class HomeFacade
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<PostInListDto>> GetHomepagePostsAsync(int count)
        {
            return await _unitOfWork.Posts.GetLastestPublishPost(count);
        }
    }
}
