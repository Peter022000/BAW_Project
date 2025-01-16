using Microsoft.AspNetCore.Identity;

namespace BAW_Project_API.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Dodanie ról
                string[] roles = { "Admin", "User" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Dodanie użytkownika Admin
                var adminLogin = "admin";
                var adminEmail = "admin@admin.com";
                var adminPassword = "adminadmin";

                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new IdentityUser
                    {
                        UserName = adminLogin,
                        Email = adminEmail
                    };

                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }
        }
    }
}
