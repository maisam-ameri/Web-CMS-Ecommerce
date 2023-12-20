using Cms.Core.Convertors;
using Cms.Core.DTOs.AdminPanel;
using Cms.Core.DTOs.Shop;
using Cms.Core.Providers;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Cms.Web.Pages.Admin.Shop.Discountcode
{
    public class AssignDiscountCodeToUserModel : PageModel
    {
        public UsersDto Users { get; set; }

        private IUserService _userService;
        private IPermissionService _permissionService;
        private IDiscountService _discountService;
        private IViewRenderService _viewRenderService;

        public AssignDiscountCodeToUserModel(IPermissionService permissionService, IDiscountService discountService,IUserService userService,IViewRenderService viewRenderService)
        {
            _permissionService = permissionService;
            _discountService = discountService;
            _userService = userService;
            _viewRenderService = viewRenderService;
        }

        public void OnGet(int? id)
        {
            var roles = new SelectList(_permissionService.GetRolesForSelectList(), "Value", "Text");
            ViewData["discountCodeId"] = id;
            ViewData["roles"] = roles;
            ViewData["selectedRoles"] = _discountService.GetRolesIdsAssignedToDiscountCode(id.Value);
        }

        public IActionResult OnPost()
        {
            var selectedRoles = Request.Form["selectedRoles"].ToString().Split(',').ToList();
            var discountCodeId = int.Parse(Request.Form["discountCodeId"]);
            _discountService.AssignDiscountCodeToUsers(discountCodeId, selectedRoles);

            // send email
            SendDiscountCodeEmail();

            return RedirectToPage("Index");
        }
        private void SendDiscountCodeEmail()
        {
            var user = _userService.GetUserByUserName(User.Identity.Name);

            var discountCodesUser = _discountService.GetDiscountCodesUserByUserId(user.Id);

            foreach (var discountCodeUser in discountCodesUser)
            {
                var discountCode = _discountService.GetDiscountCode(discountCodeUser.DiscountCodeId);

                var body = _viewRenderService.RenderToStringAsync("_DiscountCodeEmail", new DiscountCodeEmailDto
                {
                    Code = discountCodeUser.Code,
                    Title = discountCode.Title,
                    UserName = User.Identity.Name,
                });

                SendEmail.Send(user.Email, discountCode.Title, body);

                //_discountService.SendDiscountCodeEmail(discountCodeId,userName,)

            }
        }


        //public void OnGet(int pageId = 1, string emailFilter = "", string usernameFilter = "")
        //{
        //    Users = _adminService.GetUsers(pageId, emailFilter, usernameFilter);
        //}
    }
}
