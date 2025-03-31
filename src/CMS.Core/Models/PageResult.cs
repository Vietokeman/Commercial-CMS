namespace CMS.Core.Models
{
    public class PageResult<T> : PageResultBase where T : class
    {
        public List<T> Results { get; set; }
        public PageResult()
        {
            Results = new List<T>();
        }
    }
}
