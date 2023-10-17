using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Shop.Product
{
    public class DeleteProductModel : PageModel
    {
        [BindProperty]
        public Cms.DataLayer.Entities.Product.Product Product { get; set; }

        private IProductService _productService;

        public DeleteProductModel(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult OnGet(int? id)
        {
            if (id == null) return Page();

            Product = _productService.GetProduct(id.Value);

            return Page();
        }

        public IActionResult OnPost()
        {

            _productService.DeleteProduct(Product.ProductId);
            return RedirectToPage("Index");
        }
    }
}
