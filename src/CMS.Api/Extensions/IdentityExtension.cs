using CMS.Core.SeedWorks.Constants;
using System.Security.Claims;

namespace CMS.Api.Extensions
{
    public static class IdentityExtension
    {
        //this ClaimsIdentity claimsIdentity: Điều này có nghĩa là hàm này là một phương thức mở rộng cho lớp ClaimsIdentity. Bạn có thể gọi nó như một phương thức của ClaimsIdentity.
        //string claimType: Đây là loại thông tin(claim type), ví dụ như tên, email, hoặc một loại claim tùy chỉnh nào đó mà bạn muốn truy xuất.
        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);

            return (claim != null) ? claim.Value : string.Empty;
        }


        //claimsPrincipal.Identity: Truy cập thông tin xác thực của người dùng, mà trong trường hợp này là một ClaimsIdentity.
        //Single(...) : Lấy một claim đơn nhất mà loại claim của nó bằng với UserClaims.Id.Đây là cách hiệu quả để truy xuất ID của người dùng.
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = ((ClaimsIdentity)claimsPrincipal.Identity).Claims.Single(x => x.Type == UserClaims.Id);
            return Guid.Parse(claim.Value);
        }
    }
}
