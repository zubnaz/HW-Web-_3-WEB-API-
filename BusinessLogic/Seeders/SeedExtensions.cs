using BusinessLogic.ApiModels.Accounts;
using BusinessLogic.Data.Entitys;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace BusinessLogic.Seeders
{
    public static class SeedExtensions
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in Enum.GetNames(typeof(RolesAccount.Role)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            const string USERNAME = "Nazariy@admin.com";
            const string PASSWORD = "Nazariy-345";

            var existingUser = await userManager.FindByNameAsync(USERNAME);

            if (existingUser == null)
            {
                var user = new User
                {
                    UserName = USERNAME,
                    Email = USERNAME,
                    PhoneNumber="095 - 660 - 5205",
                    BirthDate = new DateTime(2006, 3, 13)
                };

                var result = await userManager.CreateAsync(user, PASSWORD);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, RolesAccount.Role.Admin.ToString());
                }
            }
        }
    }
}
