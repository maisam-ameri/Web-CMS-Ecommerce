using Cms.Core.DTOs.AdminPanel;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities;
using Cms.DataLayer.Entities.Permission;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Roles
{
    public class CreateRoleModel : PageModel
    {
        [BindProperty]
        public Role Role { get; set; }


        private IPermissionService _permissionService { get; set; }


        public CreateRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }


        public void OnGet()
        {

            ViewData["Permissions"] = _permissionService.GetPermissions();
        }

        public IActionResult OnPost(List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var roleId = _permissionService.CreateRole(Role);
            if(selectedPermissions.Count > 0)
                _permissionService.AssignPermissionToRole(roleId, selectedPermissions);

            return RedirectToPage("Index");
        }
    }
}
