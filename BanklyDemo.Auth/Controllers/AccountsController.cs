using BanklyDemo.Core.Users;
using BanklyDemo.Core.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace BanklyDemo.Auth.Controllers
{
    public class AccountController: Controller
    {
        private readonly IUserAccountService _userAccountService;
        public AccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse> Login(UserLoginModel model)
        {
           
            var authResponse = _userAccountService.LoginAsync(model);

            return Ok(authResponse);

        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new UserRegistrationModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public IActionResult Register(UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var registeredUser = _userAccountService.RegisterAsync(model, true);

            return Redirect(model.ReturnUrl);
        }
    }
}
