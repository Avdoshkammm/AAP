using AAP.Domain.Entities;
using AAP.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AAP.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> singInManager;
        public AccountRepository(UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            userManager = _userManager;
            singInManager = _signInManager;
        }

        public async Task<User> Login(User user)
        {
            User? logUser = await userManager.FindByEmailAsync(user.UserName);
            if (logUser == null)
            {
                logUser = await userManager.FindByNameAsync(user.UserName);
            }
            return logUser;
        }

        public Task PasswordSignInAsync(User user, string password, bool rememberMe, bool lockoutOnFailure)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            User? exUserByLogin = await userManager.FindByNameAsync(user.UserName);
            User? exUserByEmail = await userManager.FindByEmailAsync(user.Email);

            if(exUserByLogin == null && exUserByEmail == null)
            {
                User newUser = new User
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Pathronomic = user.Pathronomic,
                };

                var result = await userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }
            return user;
        }

        public async Task SignInAsync(User user, bool ispersistent)
        {
            await singInManager.SignInAsync(user, isPersistent: ispersistent);
        }
    }
}
