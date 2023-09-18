using Cms.Core.DTOs;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        public int Num;
        public UsersDto Users{ get; set; }

        private IAdminService _adminService;

        public IndexModel(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public void OnGet()
        {
            Users = _adminService.GetUsers();
        }
    }
}
