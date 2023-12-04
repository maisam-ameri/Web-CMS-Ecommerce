using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Shop.Discount
{
    public class IndexModel : PageModel
    {
        private IDiscountService _discountService;

        public IndexModel(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public void OnGet()
        {
            var discounts = _discountService.GetDiscounts().ToList();
            ViewData[nameof(discounts)] = discounts;
        }
    }
}
