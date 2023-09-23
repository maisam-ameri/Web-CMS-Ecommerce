using Cms.Core.DTOs.AdminPanel;
using Cms.Core.FileManager;
using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Roles
{
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
            Role = _permissionService.GetRole(id);
            return Page();
        }

        public IActionResult OnPost()
        {
            _permissionService.UpdateRole(Role);
            return RedirectToPage("Index");
        }
    }
}
