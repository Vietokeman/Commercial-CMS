namespace CMS.Core.Models.Content
{
    public class SeriesInListDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }
        public required string Description { get; set; }

        public required string Slug { get; set; }

        public bool IsActive { get; set; }

        public int SortOrder { get; set; }

        public string? SeoKeywords { get; set; }


        public Guid AuthorUserID { get; set; }
    }
}
