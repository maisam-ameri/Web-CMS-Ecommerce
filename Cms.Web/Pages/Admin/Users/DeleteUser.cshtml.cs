using Cms.Core.Security;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Users
{
    [PermissionChecker(9)]

    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;


        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnGet(int id)
        {
            ViewData["UserInfo"] = _userService.GetUserInformation(id);
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToPage("Index");
        }
    }
}
