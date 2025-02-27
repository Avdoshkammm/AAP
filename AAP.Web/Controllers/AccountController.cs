using AAP.Application.DTO;
using AAP.Application.Interfaces;
using AAP.Domain.Entities;
using AAP.Domain.Interfaces;
using AAP.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AAP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository service;
        private readonly UserManager<User> um;
        private readonly SignInManager<User> sim;
        public AccountController(IAccountRepository _service, UserManager<User> _um, SignInManager<User> _sim)
        {
            service = _service;
            um = _um;
            sim = _sim;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //с класса dto сделал user просто
        //использовал другой интерфейс
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = viewModel.Login,
                    Email = viewModel.Email,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Pathronomic = viewModel.Pathronomic
                };
                await service.Register(user, viewModel.Password);
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel vm = new LoginViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                /*
                User? user = await um.FindByEmailAsync(viewModel.Login);
                if(user == null)
                {
                    user = await um.FindByNameAsync(viewModel.Login);
                }*/

                User dbuser = new User()
                {
                    UserName = viewModel.Login,
                };

                var user = await service.Login(dbuser);

                //var loguser = await service.Login(user);
                if(user != null)
                {
                    var result = await sim.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        //HttpContext.Session.SetString("UserID", user.Id);
                        //var role = await um.GetRolesAsync(user);
                        //HttpContext.Session.SetString("UserRole", role.FirstOrDefault());
                        await SignInAsync(user);
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attemp");
                    }
                }
            }
            return View(viewModel);
        }   

        private async Task SignInAsync(User user)
        {
            await sim.SignInAsync(user, isPersistent: false);
        }
    }
}
