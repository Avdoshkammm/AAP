using AAP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAP.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(User user);
        Task SignInAsync(User user, bool ispersistent);
        Task PasswordSignInAsync(User user, string password, bool rememberMe, bool lockoutOnFailure);
        
    }
}
