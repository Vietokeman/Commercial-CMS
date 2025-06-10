using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Core.Domain.Content
{
    [Table("PostActivityLogs")]
    public class PostActivityLog
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public PostStatus FromStatus { get; set; }

        public PostStatus ToStatus { get; set; }

        public DateTime DateCreated { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        public Guid UserId { get; set; }
        public required string UserName { get; set; }
    }
}
