using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cms.Web.Pages.Admin.Shop.Discount
{
    public class EditDiscountModel : PageModel
    {
        [BindProperty]
        public Cms.DataLayer.Entities.Shop.Discount Discount { get; set; }

        private IDiscountService _discountService;
        private IProductService _productService;

        public EditDiscountModel(IDiscountService discountService, IProductService productService)
        {
            _discountService = discountService;
            _productService = productService;
        }

        public IActionResult OnGet(int id)
        {
            var products = _productService.GetProductsForDiscountEdition(id);
            ViewData[nameof(products)] = new SelectList(products, "Value", "Text");

            var discountProductIds = _discountService.GetDiscountProductIds(id);
            ViewData[nameof(discountProductIds)] = discountProductIds;
            Discount = _discountService.GetDiscount(id);
            return Page();
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var selectedProducts = Request.Form["selectedProducts"];
            _discountService.UpdateDiscount(Discount, selectedProducts);

            
            return RedirectToPage("Index");
        }

    }
}
