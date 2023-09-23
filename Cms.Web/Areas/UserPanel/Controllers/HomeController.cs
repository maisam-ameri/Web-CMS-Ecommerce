using Cms.Core.DTOs;
using Cms.Core.DTOs.UserPanel;
using Cms.Core.FileManager;
using Cms.Core.Security;
using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.Text;

namespace Cms.Web.Areas.UserPanel.Controllers
{
    [Area("userPanel")]
    public class HomeController : Controller
    {

        private IUserService _userService;
        private IWebHostEnvironment _webHostEnvironment;
        private ImageManager _imageManager;
        public HomeController(IUserService userService, IWebHostEnvironment webHostEnvironment, ImageManager imageManager)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            _imageManager = imageManager;
        }
        public IActionResult Index()
        {
            var user = _userService.GetUserInformation(User.Identity.Name);
            return View(user);
        }

        #region Edit Profile


        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            var userProfile = _userService.GetEditUserProfile(User.Identity.Name);
            return View(userProfile);
        }

        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditProfileDto profile)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }
            var user = _userService.GetUserByUserName(User.Identity.Name);

            if (_userService.IsExistUserName(profile.UserName) && profile.UserName != user.UserName)
            {
                ModelState.AddModelError("UserName", "نام کاربری وارد شده تکراری می باشد");
                return View(profile);
            }
            if (_userService.IsExistEmail(profile.Email) && profile.Email != user.Email)
            {
                ModelState.AddModelError("UserName", "ایمیل وارد شده تکراری می باشد");
                return View(profile);
            }

            var newAvatarName = _imageManager.UploadAvatar(profile.AvatarName, profile.Avatar);
            user.Avatar = newAvatarName;
            user.UserName = profile.UserName;
            user.Email = profile.Email;
            profile.AvatarName = newAvatarName;
            _userService.UpdateUser(user);

            // redirect to the login page

            return Redirect("/Logout?isProfileEdited=true");
        }

        #endregion

        #region Edit Password

        [Route("UserPanel/EditPassword")]
        public IActionResult EditPassword()
        {
            return View();
        }

        [Route("UserPanel/EditPassword")]
        [HttpPost]
        public IActionResult EditPassword(EditPasswordDto editPassword)
        {
            if(!ModelState.IsValid)
            {
                return View(editPassword);
            }

            var hashPassword = PasswordHash.EncodePasswordMd5(editPassword.CurrentPassword);
            var isPasswordCorrect = _userService.CheckCurrentPassword(User.Identity.Name, hashPassword);



            if(isPasswordCorrect)
            {
                var user = _userService.GetUserByUserName(User.Identity.Name);
                user.Password = PasswordHash.EncodePasswordMd5(editPassword.NewPassword);
                _userService.UpdateUser(user);

                ViewBag.isSuccess = true;
                return View();
            }

            return View(editPassword);
        }
        #endregion
    }
}
