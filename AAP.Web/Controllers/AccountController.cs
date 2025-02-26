using AAP.Application.DTO;
using AAP.Application.Interfaces;
using AAP.Domain.Entities;
using AAP.Domain.Interfaces;
using AAP.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AAP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository service;
        public AccountController(IAccountRepository _service)
        {
            service = _service;
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
                    Pathronomic = viewModel.Pathronomic,
                    //PasswordHash = viewModel.Password
                };
                await service.Register(user, viewModel.Password);
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }
    }
}
