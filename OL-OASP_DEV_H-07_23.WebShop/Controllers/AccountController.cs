using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.AccountModels;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;

namespace OL_OASP_DEV_H_07_23.WebShop.Controllers
{

    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationBinding model)
        {
            await accountService.CreateUser(model, Roles.Buyer);
            return RedirectToAction("Index", "Buyer");
        }

        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            var profile = await accountService.GetUserProfile<ApplicationUserUpdateBinding>(User);
            return View(profile);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MyProfile(ApplicationUserUpdateBinding model)
        {
      await accountService.UpdateUserProfile(model);
            //var profile = await accountService.GetUserProfile<ApplicationUserUpdateBinding>(User);
            //return View(profile);

            return RedirectToAction("MyProfile");
        }
    }
}
