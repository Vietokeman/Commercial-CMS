using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Core.Domain.Content
{
    [Table("Series")]
    [Index(nameof(Slug), IsUnique = true)]
    public class Series
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(250)]
        [Required]
        public required string Name { get; set; }
        [MaxLength(250)]
        public required string Description { get; set; }

        [Column(TypeName = "varchar(250)")]
        public required string Slug { get; set; }

        public bool IsActive { get; set; }

        public int SortOrder { get; set; }

        [MaxLength(250)]
        public string? SeoKeywords { get; set; }

        public string? SeoDescription { get; set; }

        public string? Thumbnail { get; set; }
        public string? Content { get; set; }
        public Guid AuthorUserID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
