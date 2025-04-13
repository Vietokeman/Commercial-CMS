namespace CMS.Core.Models.System
{
    public class PermissionDto
    {
        public string RoleId { get; set; } = string.Empty;
        public IList<RoleClaimsDto> RoleClaims { get; set; }
    }
}
