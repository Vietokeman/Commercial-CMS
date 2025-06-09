using CMS.Core.Domain.Content;

namespace CMS.Core.Models.Content
{
    public class PostActivityLogDto
    {


        public PostStatus FromStatus { get; set; }

        public PostStatus ToStatus { get; set; }

        public DateTime DateCreated { get; set; }


        public string? Note { get; set; }

        public required string UserName { get; set; }
    }
}
