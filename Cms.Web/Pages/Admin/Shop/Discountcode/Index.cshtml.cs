using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Shop.Discountcode
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
            var discountCodes = _discountService.GetDiscountCodes();
            ViewData[nameof(discountCodes)] = discountCodes;
        }
  
    }
}
