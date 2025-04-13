namespace CMS.Core.System
{
    public class PermissionDto
    {
        public string RoleId { get; set; } = string.Empty;
        public IList<RoleClaimsDto> RoleClaims { get; set; }
    }
}
