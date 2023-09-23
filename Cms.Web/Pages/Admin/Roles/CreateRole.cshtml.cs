using Cms.Core.DTOs.AdminPanel;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities;
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

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _permissionService.CreateRole(Role);
            return RedirectToPage("Index");
        }
    }
}
