using Cms.Core.Convertors;
using Cms.Core.DTOs;
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
    public class CreateUserModel : PageModel
    {
        public IPermissionService _permissionService{ get; set; }
        public IUserService _userService { get; set; }
        private IWebHostEnvironment _webHostEnvironment { get; set; }
        

        [BindProperty]
        public CreateUserDto  CreateUser{ get; set; }

        [BindProperty]
        public List<string> TestCheckboxes { get; set; }

        public CreateUserModel(IPermissionService permissionService, IUserService userService)
        {
            _permissionService = permissionService;
            _userService = userService;

        }

        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetRoles().ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _permissionService.GetRoles().ToList();
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


            var user = new User
            {
                UserName = CreateUser.UserName,
                Email = CreateUser.Email.FixEmail(),
                Password = PasswordHash.EncodePasswordMd5(CreateUser.Password),
                IsActive = true,
                Address = string.Empty,
                //Avatar = register.Avatar,
                RegisteredDate = DateTime.Now,
                ActivateCode = NameGenerator.GenerateName()
            };

            _userService.CreateUser(user);

            return Page();
        }

        private string UploadAvatar(string oldname, IFormFile file)
        {
            var avatarName = $"{Guid.NewGuid()}{DateTime.Now.ToString("yymmssfff")}{Path.GetExtension(file.FileName)}";
            var rootPath = _webHostEnvironment.WebRootPath;
            var oldPath = Path.Combine(rootPath, "images/user/avatar/", oldname);
            var path = Path.Combine(rootPath, "images/user/avatar/", avatarName.ToString());
            RemoveLastAvatar(oldPath);


            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Close();
            }

            return avatarName;
        }

        private void RemoveLastAvatar(string path)
        {
            var fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

        }
    }
}
