using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Api.Middlewares
{
    /// <summary>
    /// JWT Middleware for validating and extracting token information
    /// Follows best practices by keeping logic in Application/Service layer
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserToContext(context, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["JwtTokenSettings:Key"]);
                
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtTokenSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtTokenSettings:Issuer"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                
                // Attach user information to context for access in controllers
                context.Items["UserId"] = jwtToken.Claims.First(x => x.Type == "sub").Value;
                context.Items["UserName"] = jwtToken.Claims.First(x => x.Type == "name")?.Value;
            }
            catch
            {
                // Token validation failed - do nothing
                // User will be handled by [Authorize] attribute
            }
        }
    }
}
