using Cms.Core.Convertors;
using Cms.Core.DTOs;
using Cms.Core.DTOs.AdminPanel;
using Cms.Core.FileManager;
using Cms.Core.Generators;
using Cms.Core.Providers;
using Cms.Core.Security;
using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer;
using Cms.DataLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;

namespace Cms.Web.Pages.Admin.Users
{
    [PermissionChecker(3)]

    public class CreateUserModel : PageModel
    {
        [BindProperty]
        public CreateUserDto CreateUser { get; set; }


        private IPermissionService _permissionService { get; set; }
        private IUserService _userService { get; set; }
        private ImageManager _imageManager { get; set; }


        public CreateUserModel(IPermissionService permissionService, IUserService userService, ImageManager imageManager)
        {
            _permissionService = permissionService;
            _userService = userService;
            _imageManager = imageManager;

        }

        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetRoles().ToList();

        }

        public IActionResult OnPost(List<int> selectedRoles)
        {
            ViewData["Roles"] = _permissionService.GetRoles().ToList();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_userService.IsExistUserName(CreateUser.UserName))
            {
                ModelState.AddModelError("UserName", "این نام کاربری قبلا ثبت شده است");
                return Page();
            }
            if (_userService.IsExistEmail(CreateUser.Email))
            {
                ModelState.AddModelError("Email", "این ایمیل قبلا ثبت شده است");
                return Page();
            }

            var userAvatar = _imageManager.UploadAvatar("", CreateUser.Avatar);

            //var roles = _permissionService.GetUserRoles(CreateUser.UserName, selectedRoles).ToList();

            var user = new User
            {
                UserName = CreateUser.UserName,
                Email = CreateUser.Email.FixEmail(),
                Password = PasswordHash.EncodePasswordMd5(CreateUser.Password),
                IsActive = true,
                Address = string.Empty,
                Avatar = userAvatar,
                RegisteredDate = DateTime.Now,
                ActivateCode = NameGenerator.GenerateName(),
                //UserRoles = roles
                FirstName = string.Empty,
                LastName = string.Empty,
                BirthDate = DateTime.Now,
                Phone = string.Empty,
            };

            _userService.CreateUser(user);
            _permissionService.AssignUserRoles(user.Id, selectedRoles);

            return Redirect("/Admin/Users");
        }
    }
}
