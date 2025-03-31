namespace CMS.Core.Models.Content
{
    public class CreateUpdatePostRequest
    {
        public required string Name { get; set; }
        public required string Slug { get; set; } // duong dan than thien
        public string? Decription { get; set; }
        public Guid CategoryId { get; set; }
        public string? Thumbnail { get; set; }
        private string? Content { get; set; }
        public string? Source { get; set; }
        public string? Tags { get; set; }
        public string? SeoDescription { get; set; }
        // tiền nhuận bút
    }
}
