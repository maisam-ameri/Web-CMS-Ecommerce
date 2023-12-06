using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Shop.Discount
{
    public class DeleteDiscountModel : PageModel
    {
        [BindProperty]
        public Cms.DataLayer.Entities.Shop.Discount Discount { get; set; }

        private IDiscountService _discountService;


        public DeleteDiscountModel(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null) return Page();

            Discount = _discountService.GetDiscount(id.Value);

            return Page();
        }


        public IActionResult OnPost()
        {
            _discountService.DeleteDiscount(Discount.DiscountId);

            return RedirectToPage("Index");
        }
    }
}
