using CMS.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace CMS.WebApp.Helpers
{

    public class CustomClaimsPrincipalFactory :
    UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        private readonly UserManager<AppUser> _userManager;

        public CustomClaimsPrincipalFactory(
           UserManager<AppUser> userManager,
           RoleManager<AppRole> roleManager,
           IOptions<IdentityOptions> optionsAccessor) :
              base(userManager, roleManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            // Add your claims here
            //((ClaimsIdentity)principal.Identity)?.AddClaims(new[] {
            //    new Claim(UserClaims.Id, user.Id.ToString()),
            //    new Claim(UserClaims.Code, user.Code.ToString()),
            //    new Claim(ClaimTypes.Email, user.Email),
            //    new Claim(UserClaims.FirstName, user.FirstName),
            //    new Claim(UserClaims.LastName, user.LastName),
            //    new Claim(UserClaims.Avatar, string.IsNullOrEmpty(user.Avatar)? string.Empty : user.Avatar),
            //    new Claim(UserClaims.Roles, string.Join(";",roles)),
            //    new Claim(UserClaims.VipExpiredDate, user.VipExpireDate.HasValue? user.VipExpireDate.Value.ToString("yyyy-MM-dd"): string.Empty)
            //});
            return principal;
        }
    }
}