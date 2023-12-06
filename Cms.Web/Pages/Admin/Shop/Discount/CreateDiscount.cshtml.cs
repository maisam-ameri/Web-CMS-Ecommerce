using Cms.Core.DTOs.Shop;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cms.Web.Pages.Admin.Shop.Discount
{
    public class CreateDiscountModel : PageModel
    {
        [BindProperty]
        public DiscountDto Discount { get; set; }

        private IDiscountService _discountService;
        private IProductService _productService;

        public CreateDiscountModel(IDiscountService discountService, IProductService productService)
        {
            _discountService = discountService;
            _productService = productService;

        }

        public void OnGet()
        {
            var products = _productService.GetProductsForSelectList();
            ViewData[nameof(products)] = new SelectList(products, "Value", "Text");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Discount.ProductIds = Request.Form["selectedProducts"];
            _discountService.CreateDiscount(Discount);

            return RedirectToPage("Index");
        }
    }
}
