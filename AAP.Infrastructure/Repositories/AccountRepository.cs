using AAP.Domain.Entities;
using AAP.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
