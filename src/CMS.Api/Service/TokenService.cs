using CMS.Core.ConfigOptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CMS.Api.Service
{
    public class TokenService : ITokenService
    {
        private readonly JwtTokenSettings _jwtTokenSettings;
        public TokenService(IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            _jwtTokenSettings = jwtTokenSettings.Value;
        }
        //Token bao gom Header, Payload, Signature
        //Header : chua thuat toan va type token o day la HS256 va JWT
        //Payload chua: claims 
        //signature tu dong bang cach ma hoa header + payload 
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            //tao ra 1 doi tuong khoa bi mat token bang thuat toan hash sha256 
            //.getbytes chuyen sang bytes 
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            // tao ra chu ki va ki vao payload
            var tokeOptions = new JwtSecurityToken
                (
                issuer: _jwtTokenSettings.Issuer, //set up trong appsetings
                audience: _jwtTokenSettings.Issuer, //appsetings
                claims: claims,
                expires: DateTime.Now.AddHours(_jwtTokenSettings.ExpireInHours), // them thoi gian cong voi thoi gian token het han
                signingCredentials: signinCredentials // ki token bang signature da tao o tren
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// Get user principle from expired token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenException"></exception>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.Key)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }


    }
}
