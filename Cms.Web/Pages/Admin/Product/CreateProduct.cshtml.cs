using Cms.Core.DTOs.AdminPanel.Product;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Product
{
    public class CreateProductModel : PageModel
    {
        [BindProperty]
        public ProductDto Product { get; set; }

        private IProductService _productService;

        public CreateProductModel(IProductService productService)
        {
            _productService = productService;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var product = new Cms.DataLayer.Entities.Product.Product
            {
                Title = Product.Title,
                Description = Product.Description,
                IsDeleted = false,
                
            };

            _productService.CreateProduct(product);

            return RedirectToPage("Index");
        }

    }
}
