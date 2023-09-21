using Cms.Core.DTOs;
using Cms.Core.DTOs.AdminPanel;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        public UsersDto Users{ get; set; }

        private IAdminService _adminService;

        public IndexModel(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public void OnGet(int pageId = 1,string emailFilter="",string usernameFilter = "")
        {
            Users = _adminService.GetUsers(pageId,emailFilter, usernameFilter);
        }
    }
}
