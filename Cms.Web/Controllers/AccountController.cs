using Cms.Core;
using Cms.Core.Convertors;
using Cms.Core.Generators;
using Cms.Core.Security;
using Cms.Core.Services;
using Cms.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "این نام کاربری قبلا ثبت شده است");
                return View(register);
            }
            if (_userService.IsExistEmail(register.Email))
            {
                ModelState.AddModelError("Email", "این ایمیل قبلا ثبت شده است");
                return View(register);
            }


            var user = new User
            {
                UserName = register.UserName,
                Email = register.Email.FixEmail(),
                Password = PasswordHash.CreateHash(register.Password),
                IsActive = false,
                Address = register.Address,
                Avatar = register.Avatar,
                BirthDate = register.BirthDate,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Phone = register.Phone,
                RegisteredDate = DateTime.Now,
                ActivateCode = NameGenerator.GenerateName()
            };

            _userService.CreateUser(user);

            return View("RegisterCompleted", user);
        }
    }
}
