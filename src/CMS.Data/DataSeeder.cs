using CMS.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace CMS.Data
{
    public class DataSeeder
    {
        public async Task SeedAsync(CMSDbContext context)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            var rootAdminRoleId = Guid.NewGuid();
            if (!context.Roles.Any())
            {
                await context.Roles.AddAsync(new AppRole()
                {
                    Id = rootAdminRoleId,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                    DisplayName = "Quản trị viên",
                });
                await context.SaveChangesAsync();
            }
            if (!context.Users.Any())
            {
                var userId = Guid.NewGuid();
                var user = new AppUser()
                {
                    Id = userId,
                    FirstName = "Viet",
                    LastName = "Admin",
                    NormalizedEmail = "VIETBMT19@GMAIL.COM",
                    UserName = "VietAdmin",
                    NormalizedUserName = "VIETADMIN",
                    IsActive = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                    DateCreated = DateTime.UtcNow,
                };
                user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123$");
                await context.Users.AddAsync(user);
                await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
                {
                    RoleId = rootAdminRoleId,
                    UserId = userId,
                });
                await context.SaveChangesAsync();
            }

        }
    }
}
