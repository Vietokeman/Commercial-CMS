using CMS.Api.Extensions;
using CMS.Api.Models;
using CMS.Api.Service;
using CMS.Core.Domain.Identity;
using CMS.Core.Models.Auth;
using CMS.Core.Models.System;
using CMS.Core.SeedWorks.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace CMS.Api.Controllers.AdminApi
{
    [Route("api/admin/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<AppRole> _roleManager;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            //authentication
            if (request == null)
            {
                return BadRequestResponse("Invalid request");
            }


            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null || user.IsActive == false || user.LockoutEnabled)
            {
                return UnauthorizedResponse("Invalid credentials or account is locked");
            }
            // k co await thi k the succeeded
            var loginResult = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);

            if (!loginResult.Succeeded)
            {
                return UnauthorizedResponse("Invalid credentials");
            }

            //authorization
            var roles = await _userManager.GetRolesAsync(user);

            var permissions = await this.GetPermissionsByUserIdAsync(user.Id.ToString());
            var claims = new[]
            {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                    new Claim(UserClaims.Id, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(UserClaims.FirstName, user.FirstName),
                    new Claim(UserClaims.Roles, string.Join(";", roles)),
                    new Claim(UserClaims.Permissions, JsonSerializer.Serialize(permissions)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var accesToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(30);
            await _userManager.UpdateAsync(user);

            var authResult = new AuthenticatedResult()
            {
                Token = accesToken,
                RefreshToken = refreshToken
            };

            return SuccessResponse(authResult, "Login successful");
        }
        private async Task<List<string>> GetPermissionsByUserIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var permissions = new List<string>();
            var allPermissions = new List<RoleClaimsDto>();
            if (roles.Contains(Roles.Admin))
            {
                var types = typeof(Permissions).GetNestedTypes();
                foreach (var type in types)
                {
                    allPermissions.GetPermissions(type);
                }
                permissions.AddRange(allPermissions.Select(x => x.Value));

            }
            else
            {
                foreach (var roleName in roles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    var claims = await _roleManager.GetClaimsAsync(role);
                    var roleClaimsValues = claims.Select(x => x.Value).ToList();
                    permissions.AddRange(roleClaimsValues);
                    //add range them nhieu doi tuong vao trong db
                    //add them 1 doi tuong vao ben trong db

                }
            }
            return permissions.Distinct().ToList();

        }
    }
}
