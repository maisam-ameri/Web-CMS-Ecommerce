using Cms.Core.DTOs.Shop;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cms.Web.Pages.Admin.Shop.Discountcode
{
    public class CreateDiscountCodeModel : PageModel
    {
        [BindProperty]
        public DiscountCode DiscountCode { get; set; }

        private IDiscountService _discountService;
        private IProductService _productService;
        private IUserService _userService;

        public CreateDiscountCodeModel(IDiscountService discountService, IProductService productService, IUserService userService)
        {
            _discountService = discountService;
            _productService = productService;
            _userService = userService;
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

            _discountService.CreateDiscountCode(DiscountCode);

            return RedirectToPage("Index");
        }
    }
}
