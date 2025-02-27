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
        private readonly SignInManager<User> sim;
        public AccountController(IAccountRepository _service, SignInManager<User> _sim)
        {
            service = _service;
            sim = _sim;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

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
        public async Task<IActionResult> Login(LoginViewModel LVM)
        {
            if (ModelState.IsValid)
            {
                User dbUser = new User()
                {
                    UserName = LVM.Login
                };
                var user = await service.Login(dbUser);
                if(user != null)
                {
                    var result = await sim.PasswordSignInAsync(user, LVM.Password, LVM.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        await service.SignInAsync(user, ispersistent: false);
                        //await SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attemp");
                        return RedirectToAction("Privacy", "Home");
                    }
                }
            }
            return View(LVM);
        }

        //private async Task SignInAsync(User user, bool ispersistent)
        //{
        //    await sim.SignInAsync(user, isPersistent: false);
        //}
    }
}