using AAP.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AAP.Infrastructure.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbInitializer>>();
            string[] roles = { "Admin", "User" };
            foreach(var role in roles )
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    logger.LogInformation("Роль успешно создана");
                }
                else
                {
                    logger.LogInformation("Ошибка создания роли");
                }
            }

            string aEmail = "Admin@mail.ru";
            string aPassword = "@dminPassword667";
            var adminUser = await userManager.FindByNameAsync(aEmail);
            if(adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "Admin",
                    Email = aEmail,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Pathronomic = "Admin",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(adminUser, aPassword);
                if (result.Succeeded)
                {
                    var addToRoleAsync = await userManager.AddToRoleAsync(adminUser, "Admin");
                    if (addToRoleAsync.Succeeded)
                    {
                        logger.LogInformation("Пользователь успешно создан");
                    }
                    else
                    {
                        logger.LogInformation("Ошибка при создании роли : " + string.Join(" ", addToRoleAsync.Errors));
                    }
                }
                else
                {
                    logger.LogInformation("Пользователь с таким email уже существует");
                }
            }
            else
            {
                logger.LogInformation("Ошибка создания пользователя");
            }
        }
    }
}
