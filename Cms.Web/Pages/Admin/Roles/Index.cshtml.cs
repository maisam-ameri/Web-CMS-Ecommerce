using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Roles
{
    public class IndexModel : PageModel
    {
        private IPermissionService  _permissionService{ get; set; }

        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public List<Role> Roles  { get; set; }
        public void OnGet()
        {
            Roles = _permissionService.GetRoles().ToList();

        }
    }
}
