using Cms.Core.Security;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Roles
{
    [PermissionChecker(8)]
    public class DeleteRoleModel : PageModel
    {
        [BindProperty]
        public Role Role { get; set; }

        private IPermissionService _permissionService;

        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public void OnGet(int id)
        {
            Role = _permissionService.GetRole(id);
        }

        public IActionResult OnPost()
        {
            _permissionService.DeleteRole(Role.RoleId);
            return RedirectToPage("Index");
        }
    }
}
