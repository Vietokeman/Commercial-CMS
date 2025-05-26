namespace CMS.Core.Models.Content
{
    public class PostInListDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Slug { get; set; } // duong dan than thien
        public string? Decription { get; set; }
        public Guid CategoryId { get; set; }
        public string? Thumbnail { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }

        public required string CategorySlug { get; set; }

        public required string CategoryName { get; set; }

        public required string AuthorName { get; set; }
        public required string AuthorUserName { get; set; }
    }
}