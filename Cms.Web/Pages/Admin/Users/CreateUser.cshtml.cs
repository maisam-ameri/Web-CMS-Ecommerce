using Cms.Core.DTOs;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Users
{
    public class CreateUserModel : PageModel
    {
        public IPermissionService _permissionService{ get; set; }
        public CreateUserModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public CreateUserDto  CreateUser{ get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetRoles().ToList();
        }
    }
}
