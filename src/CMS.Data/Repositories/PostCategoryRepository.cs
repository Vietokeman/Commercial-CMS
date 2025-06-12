using AutoMapper;
using CMS.Core.Domain.Content;
using CMS.Core.Models;
using CMS.Core.Models.Content;
using CMS.Core.Repositories;
using CMS.Data.SeedWorks;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data.Repositories
{
    public class PostCategoryRepository : RepositoryBase<PostCategory, Guid>, IPostCategoryRepository
    {
        private readonly IMapper _mapper;
        public PostCategoryRepository(CMSDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<PostCategoryDto> GetBySlug(string slug)
        {
            var category = await _context.PostCateGories
                .FirstOrDefaultAsync(x => x.Slug == slug);
            if (category == null)
            {
                return null;
            }
            return _mapper.Map<PostCategoryDto>(category);
        }

        public async Task<PageResult<PostCategoryDto>> GetPostCategorysPagingAsync(string? keyword, int PageIndex = 1, int pageSize = 10)
        {
            var query = _context.PostCateGories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }
            var totalRow = await query.CountAsync();

            query = query.OrderByDescending(x => x.DateCreated).Skip((PageIndex - 1) * pageSize).Take(pageSize);

            return new PageResult<PostCategoryDto>
            {
                Results = await _mapper.ProjectTo<PostCategoryDto>(query).ToListAsync(),
                CurrentPage = PageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };
        }
    }

}
