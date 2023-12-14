using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Shop.Discountcode
{
    public class DeleteDiscountCodeModel : PageModel
    {
        [BindProperty]
        public DiscountCode DiscountCode { get; set; }

        private IDiscountService _discountService;


        public DeleteDiscountCodeModel(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null) return Page();

            DiscountCode = _discountService.GetDiscountCode(id.Value);

            return Page();
        }


        public IActionResult OnPost()
        {
            _discountService.DeleteDiscountCode(DiscountCode.DiscountCodeId);

            return RedirectToPage("Index");
        }
    }
}
