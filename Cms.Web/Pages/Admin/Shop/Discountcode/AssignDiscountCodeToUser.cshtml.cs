using Cms.Core.DTOs.AdminPanel;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Shop.Discountcode
{
    public class AssignDiscountCodeToUserModel : PageModel
    {
        public UsersDto Users { get; set; }

        private IAdminService _adminService;

        public AssignDiscountCodeToUserModel(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public void OnGet(int pageId = 1, string emailFilter = "", string usernameFilter = "")
        {
            Users = _adminService.GetUsers(pageId, emailFilter, usernameFilter);
        }
    }
}
