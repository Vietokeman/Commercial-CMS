using CMS.Core.Domain.Content;
using CMS.Core.Models;
using CMS.Core.Models.Content;
using CMS.Core.Repositories;
using CMS.Core.SeedWorks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CMS.Data.Repositories
{
    public class MockPostRepository : IPostRepository
    {
        private readonly List<Post> _mockPosts;
        private readonly List<PostCategory> _mockCategories;

        public MockPostRepository()
        {
            // Initialize mock categories
            _mockCategories = new List<PostCategory>
            {
                new PostCategory
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Technology",
                    Slug = "technology",
                    IsActive = true,
                    DateCreated = DateTime.Now.AddMonths(-6)
                },
                new PostCategory
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Lifestyle",
                    Slug = "lifestyle",
                    IsActive = true,
                    DateCreated = DateTime.Now.AddMonths(-5)
                },
                new PostCategory
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Business",
                    Slug = "business",
                    IsActive = true,
                    DateCreated = DateTime.Now.AddMonths(-4)
                }
            };

            // Initialize mock posts
            _mockPosts = new List<Post>
            {
                new Post
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Name = "Introduction to Microservices Architecture",
                    Slug = "introduction-to-microservices-architecture",
                    Description = "Learn about microservices architecture and its benefits",
                    Thumbnail = "https://via.placeholder.com/800x400/007bff/ffffff?text=Microservices",
                    Content = @"<h1>Microservices Architecture</h1>
                        <p>Microservices architecture is a method of developing software systems...</p>
                        <h2>Benefits</h2>
                        <ul>
                            <li>Scalability</li>
                            <li>Flexibility</li>
                            <li>Independent deployment</li>
                        </ul>",
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    AuthorUserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    AuthorName = "admin",
                    AuthorUserName = "admin",
                    ViewCount = 1250,
                    DateCreated = DateTime.Now.AddDays(-30),
                    Status = PostStatus.Published,
                    IsPaid = true,
                    PaidDate = DateTime.Now.AddDays(-25),
                    RoyaltyAmount = 50000,
                    Tags = "microservices,architecture,cloud"
                },
                new Post
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Name = "10 Tips for Healthy Living",
                    Slug = "10-tips-for-healthy-living",
                    Description = "Practical tips to maintain a healthy lifestyle",
                    Thumbnail = "https://via.placeholder.com/800x400/28a745/ffffff?text=Healthy+Living",
                    Content = @"<h1>Healthy Living Tips</h1>
                        <p>Living a healthy lifestyle is important for everyone...</p>
                        <ol>
                            <li>Exercise regularly</li>
                            <li>Eat balanced meals</li>
                            <li>Get enough sleep</li>
                            <li>Stay hydrated</li>
                        </ol>",
                    CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    AuthorUserId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    AuthorName = "Jane Smith",
                    AuthorUserName = "janesmith",
                    ViewCount = 892,
                    DateCreated = DateTime.Now.AddDays(-20),
                    Status = PostStatus.Published,
                    IsPaid = true,
                    PaidDate = DateTime.Now.AddDays(-15),
                    RoyaltyAmount = 35000,
                    Tags = "health,lifestyle,wellness"
                },
                new Post
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    Name = "Digital Marketing Strategies for 2025",
                    Slug = "digital-marketing-strategies-2025",
                    Description = "Latest digital marketing trends and strategies",
                    Thumbnail = "https://via.placeholder.com/800x400/ffc107/000000?text=Digital+Marketing",
                    Content = @"<h1>Digital Marketing in 2025</h1>
                        <p>The digital marketing landscape is constantly evolving...</p>
                        <h2>Key Strategies</h2>
                        <ul>
                            <li>AI-powered personalization</li>
                            <li>Video content marketing</li>
                            <li>Influencer partnerships</li>
                        </ul>",
                    CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    AuthorUserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    AuthorName = "Mike Johnson",
                    AuthorUserName = "mikej",
                    ViewCount = 567,
                    DateCreated = DateTime.Now.AddDays(-10),
                    Status = PostStatus.Published,
                    IsPaid = false,
                    Tags = "marketing,digital,business,2025"
                },
                new Post
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-ddddddddddde"),
                    Name = "Getting Started with Angular",
                    Slug = "getting-started-with-angular",
                    Description = "A comprehensive guide to Angular framework",
                    Thumbnail = "https://via.placeholder.com/800x400/dc3545/ffffff?text=Angular",
                    Content = @"<h1>Angular Framework</h1>
                        <p>Angular is a popular TypeScript-based web application framework...</p>",
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    AuthorUserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    AuthorName = "admin",
                    AuthorUserName = "admin",
                    ViewCount = 234,
                    DateCreated = DateTime.Now.AddDays(-5),
                    Status = PostStatus.WaitingForApproval,
                    IsPaid = false,
                    Tags = "angular,typescript,frontend"
                },
                new Post
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeef"),
                    Name = "The Future of AI",
                    Slug = "the-future-of-ai",
                    Description = "Exploring the future possibilities of artificial intelligence",
                    Thumbnail = "https://via.placeholder.com/800x400/6c757d/ffffff?text=AI+Future",
                    Content = @"<h1>Artificial Intelligence: The Future</h1>
                        <p>AI is transforming every aspect of our lives...</p>",
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    AuthorUserId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    AuthorName = "Jane Smith",
                    AuthorUserName = "janesmith",
                    ViewCount = 0,
                    DateCreated = DateTime.Now.AddDays(-2),
                    Status = PostStatus.Draft,
                    IsPaid = false,
                    Tags = "ai,technology,future"
                }
            };
        }

        public void Add(Post post)
        {
            post.Id = Guid.NewGuid();
            post.DateCreated = DateTime.Now;
            _mockPosts.Add(post);
        }

        public Task<List<Post>> GetPopularPostsAsync(int count)
        {
            var posts = _mockPosts
                .Where(x => x.Status == PostStatus.Published)
                .OrderByDescending(x => x.ViewCount)
                .Take(count)
                .ToList();
            return Task.FromResult(posts);
        }

        public Task<Post> GetByIdAsync(Guid id)
        {
            var post = _mockPosts.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(post);
        }

        public void Remove(Post post)
        {
            _mockPosts.Remove(post);
        }

        public Task<PagedResult<PostInListDto>> GetPostsPagingAsync(string? keyword, Guid? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var query = _mockPosts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword) || x.Tags.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
            }

            var totalRow = query.Count();
            var posts = query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new PostInListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Thumbnail = x.Thumbnail,
                    ViewCount = x.ViewCount,
                    AuthorUserName = x.AuthorUserName,
                    CategoryName = _mockCategories.FirstOrDefault(c => c.Id == x.CategoryId)?.Name ?? "",
                    CategoryId = x.CategoryId,
                    DateCreated = x.DateCreated,
                    IsPaid = x.IsPaid,
                    RoyaltyAmount = x.RoyaltyAmount,
                    Status = x.Status
                })
                .ToList();

            return Task.FromResult(new PagedResult<PostInListDto>
            {
                Results = posts,
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            });
        }

        public Task<bool> IsSlugAlreadyExisted(string slug, Guid? currentId = null)
        {
            if (currentId.HasValue)
            {
                return Task.FromResult(_mockPosts.Any(x => x.Slug == slug && x.Id != currentId.Value));
            }
            return Task.FromResult(_mockPosts.Any(x => x.Slug == slug));
        }

        public Task<List<Post>> GetListUnpaidPublishPosts(Guid userId)
        {
            var posts = _mockPosts.Where(x => x.AuthorUserId == userId 
                && x.Status == PostStatus.Published 
                && x.IsPaid == false).ToList();
            return Task.FromResult(posts);
        }

        // Not implemented for mock
        public Task<List<SeriesInListDto>> GetAllSeries(Guid postId) => Task.FromResult(new List<SeriesInListDto>());
        public Task<bool> HasPublishedInSeries(Guid seriesId) => Task.FromResult(false);
    }
}
