namespace CMS.Core.Models.System
{
    public class ChangeMyPasswordRequest
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
