using Cms.Core.DTOs.AdminPanel;
using Cms.Core.Security;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Users
{
    [PermissionChecker(2)]

    public class DeletedUsersModel : PageModel
    {
        public UsersDto Users { get; set; }

        private IAdminService _adminService;

        public DeletedUsersModel(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public void OnGet(int pageId = 1, string emailFilter = "", string usernameFilter = "")
        {
            Users = _adminService.GetDeletedUsers(pageId, emailFilter, usernameFilter);
        }
    }
}
