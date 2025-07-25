﻿using CMS.Core.Domain.Content;
using CMS.Core.Models;
using CMS.Core.Models.Content;
using CMS.Core.SeedWorks;

namespace CMS.Core.Repositories
{
    public interface IPostRepository : IRepository<Post, Guid>
    {
        Task<PageResult<PostInListDto>> GetPostsByCategoryPaging(string categorySlug, int PageIndex = 1, int pageSize = 10);
        Task<bool> IsSlugAlreadyExisted(string slug, Guid? currentId = null);
        Task<List<SeriesInListDto>> GetAllSeries(Guid postId);
        Task Approve(Guid id, Guid currentUserId);
        Task SendToApprove(Guid id, Guid currentUserId);
        Task ReturnBack(Guid id, Guid currentUserId, string note);
        Task<string> GetReturnReason(Guid id);
        Task<bool> HasPublishInLast(Guid id);
        Task<List<PostActivityLogDto>> GetActivityLogs(Guid id);
        Task<List<Post>> GetListUnpaidPublishPosts(Guid userId);
        Task<List<PostInListDto>> GetLastestPublishPost(int top);

        Task<PageResult<PostInListDto>> GetAllPaging(string? keyword, Guid currentUserId, Guid? categoryId, int PageIndex = 1, int pageSize = 10);
        Task<PostDto> GetBySlug(string slug);
        Task<List<PostInListDto>> SearchPostsBySlugAsync(string slug);
        Task<List<PostInListDto>> SearchPostsByCategorySlugAsync(string categorySlug);

    }
}
