using Cms.Core.DTOs.AdminPanel;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cms.Web.Pages.Admin.Shop.Discountcode
{
    public class AssignDiscountCodeToUserModel : PageModel
    {
        public UsersDto Users { get; set; }

        private IPermissionService _permissionService;
        private IDiscountService _discountService;

        public AssignDiscountCodeToUserModel(IPermissionService permissionService, IDiscountService discountService)
        {
            _permissionService = permissionService;
            _discountService = discountService;
        }

        public void OnGet(int? id)
        {
            var roles = new SelectList( _permissionService.GetRolesForSelectList(),"Value","Text");
            ViewData["discountCodeId"] = id;
            ViewData["roles"] = roles; 
        }

        public IActionResult OnPost()
        {
            var selectedRoles = Request.Form["selectedRoles"].ToString().Split(',').ToList();
            var discountCodeId = int.Parse( Request.Form["discountCodeId"]);
            _discountService.AssignDiscountCodeToUsers(discountCodeId, selectedRoles);
            return RedirectToPage("Index");
        }

        //public void OnGet(int pageId = 1, string emailFilter = "", string usernameFilter = "")
        //{
        //    Users = _adminService.GetUsers(pageId, emailFilter, usernameFilter);
        //}
    }
}
