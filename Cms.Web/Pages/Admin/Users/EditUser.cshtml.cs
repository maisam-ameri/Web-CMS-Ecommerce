using Cms.Core.Convertors;
using Cms.Core.DTOs.AdminPanel;
using Cms.Core.FileManager;
using Cms.Core.Generators;
using Cms.Core.Security;
using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Users
{
    public class EditUserModel : PageModel
    {
        [BindProperty]
        public EditUserDto EditUser { get; set; }

        private IAdminService _adminService;
        private IUserService _userService;
        private IPermissionService _permissionService;
        private ImageManager _imageManager;



        public EditUserModel(IAdminService adminService, IPermissionService permissionService, IUserService userService, ImageManager imageManager)
        {
            _adminService = adminService;
            _permissionService = permissionService;
            _userService = userService;
            _imageManager = imageManager;
        }

        public IActionResult OnGet(int userId)
        {
            EditUser = _adminService.GetUserForEdit(userId);

            ViewData["Roles"] = _permissionService.GetRoles().ToList();
            return Page();
        }



        public IActionResult OnPost(List<int> selectedRoles)
        {
            ViewData["Roles"] = _permissionService.GetRoles().ToList();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userService.GetUserByUserName(EditUser.UserName);
            
            string userAvatar = string.Empty;

            if (EditUser.Avatar != null)
            {
                userAvatar = _imageManager.UploadAvatar("", EditUser.Avatar);
            }
            else
            {
                userAvatar = user.Avatar;
            }

            string hashedPass = string.Empty;

            if(!string.IsNullOrEmpty(EditUser.Password))
            {
                hashedPass = PasswordHash.EncodePasswordMd5(EditUser.Password);
            }
            else
            {
                hashedPass = user.Password;
            }

            user.Email = EditUser.Email.FixEmail();
            user.Password = hashedPass;
            user.Avatar = userAvatar;


            _permissionService.AssignUserRoles(user.Id, selectedRoles);
            _userService.UpdateUser(user);

            return Redirect("/Admin/Users");
        }
    }
}
