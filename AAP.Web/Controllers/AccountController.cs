using AAP.Application.DTO;
using AAP.Application.Interfaces;
using AAP.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AAP.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService service;
        public AccountController(IAccountService _service)
        {
            service = _service;
        }
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                UserDTO user = new UserDTO()
                {
                    uUserName = viewModel.Login,
                    uEmail = viewModel.Email,
                    uFirstName = viewModel.FirstName,
                    uLastName = viewModel.LastName,
                    uPathronomic = viewModel.Pathronomic,
                };
                await service.Register(user, viewModel.Password);
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }
    }
}
