using Cms.Core.DTOs.AdminPanel;
using Cms.Core.FileManager;
using Cms.Core.Security;
using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Roles
{

    [PermissionChecker(7)]
    public class EditRoleModel : PageModel
    {
        [BindProperty]
        public Role Role { get; set; }

        private IPermissionService _permissionService;

        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public IActionResult OnGet(int id)
        {
            ViewData["Permissions"] = _permissionService.GetPermissions().ToList();
            ViewData["SelectedPermissions"] = _permissionService.GetPermissionsRole(id);
            Role = _permissionService.GetRole(id);
            return Page();
        }

        public IActionResult OnPost(List<int> selectedPermissions)
        {
            if (!ModelState.IsValid){
                return Page();
            }
            _permissionService.UpdateRole(Role, selectedPermissions);
            return RedirectToPage("Index");
        }
    }
}
