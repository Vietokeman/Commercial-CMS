using CMS.Core.Domain.Content;

namespace CMS.Core.Models.Content
{
    public class PostDto : PostInListDto
    {
        private string? Content { get; set; }

        public Guid AuthorUserId { get; set; }

        public string? Source { get; set; }
        public string? Tags { get; set; }
        public string? SeoDescription { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsPaid { get; set; }
        public double? RoyaltyAmount { get; set; }
        public PostStatus Status { get; set; }

    }
}
