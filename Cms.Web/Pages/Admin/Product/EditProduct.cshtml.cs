using Cms.Core.DTOs.AdminPanel.Product;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Product
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
            Product = _productService.GetProductForEditInAdmin(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _productService.UpdateProduct(Product);

            return RedirectToPage("Index");
        }
    }
}
