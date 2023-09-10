using Cms.Core;
using Cms.Core.Convertors;
using Cms.Core.Generators;
using Cms.Core.Security;
using Cms.Core.Services;
using Cms.DataLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cms.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }



        #region Register

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
                Password = PasswordHash.EncodePasswordMd5(register.Password),
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

        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginDto login)
        {

            if (!ModelState.IsValid)
            {
                return View(login);

            }

            var user = _userService.LoginUser(login);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "اطلاعات وارد شده نا معتبر است");
                return View(login);
            }

            if (user.IsActive)
            {

                // TODO: Login the user
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.FirstName.ToString()));

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var pricipal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = login.RememberMe,
                };

                 HttpContext.SignInAsync(pricipal, properties);


                ViewBag.LoginSuccessed = true;
                //return View(login);
            }
            else
            {
                ModelState.AddModelError("UserName", "حساب کاربری شما فعال نمی باشد");
            }


            return View(login);
        }


        #endregion

        #region Active Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }

        #endregion

        #region Logout

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion

    }
}
