using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cms.Web.Pages.Admin.Shop.Discountcode
{
    public class EditDiscountCodeModel : PageModel
    {
        [BindProperty]
        public DiscountCode DiscountCode { get; set; }

        private IDiscountService _discountService;

        public EditDiscountCodeModel(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public IActionResult OnGet(int id)
        {
            DiscountCode = _discountService.GetDiscountCode(id);
            return Page();
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _discountService.UpdateDiscountCode(DiscountCode);


            return RedirectToPage("Index");
        }
    }
}
