using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Core.Domain.Content
{
    [Table("Posts")]
    [Index(nameof(Slug), IsUnique = true)]
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(250)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(250)]
        [Column(TypeName = "varchar(250)")]
        public required string Slug { get; set; } // duong dan than thien
        [MaxLength(500)]
        public string? Decription { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [MaxLength(500)]
        public string? Thumbnail { get; set; }
        public string? Content { get; set; }
        [MaxLength(250)]

        public Guid AuthorUserId { get; set; }
        [MaxLength(128)]

        public string? Source { get; set; }
        [MaxLength(250)]
        public string? Tags { get; set; }
        [MaxLength(128)]
        public string? SeoDescription { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsPaid { get; set; }
        public double? RoyaltyAmount { get; set; }
        // tiền nhuận bút
        public PostStatus Status { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public required string CategorySlug { get; set; }
        [Required]
        [MaxLength(250)]

        public required string CategoryName { get; set; }
        [Required]

        public required string AuthorName { get; set; }
        public required string AuthorUserName { get; set; }
        public DateTime? PaidDate { get; set; }
    }


    public enum PostStatus
    {
        Draft = 0,
        Pending = 1,
        Rejected = 2,
        Published = 3,

    }
}
