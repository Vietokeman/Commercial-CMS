using System.Security.Claims;

namespace CMS.Api.Service
{
    public interface ITokenService
    {
        //Enumerable khong ho tro them xoa sua truc tiep readonly
        //Enumerable k doc doc [] so thu tu phan tu
        //Tiet kiem bo nho hon list
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
