using Cms.Core.DTOs.AdminPanel.Product;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cms.Web.Pages.Admin.Shop.Product
{
    public class EditProductModel : PageModel
    {
        [BindProperty]
        public ProductDto Product { get; set; }

        private IProductService _productService;
       

        public EditProductModel(IProductService productService, ImageManager imageManager)
        {
            _productService = productService;

        }

        public void OnGet(int id)
        {
            var categories = _productService.GetCategoryDtos();
            ViewData["categories"] = new SelectList(categories, "Id", "Title");
            Product = _productService.GetProductForEditInAdmin(id);
        }

        public IActionResult OnPost(string? categoryId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Product.CategoryId = int.Parse( categoryId);

            _productService.UpdateProduct(Product);

            return RedirectToPage("Index");
        }
    }
}
