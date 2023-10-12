using Cms.Core.DTOs.AdminPanel.Product;
using Cms.Core.FileManager;
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
        private ImageManager _imageManager { get; set; }
        private const string PRODUCT_PATH = "images/product/";

        public CreateProductModel(IProductService productService, ImageManager imageManager)
        {
            _productService = productService;
            _imageManager = imageManager;

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

            var userAvatar = _imageManager.UploadImage("", Product.Image,PRODUCT_PATH);


            var product = new Cms.DataLayer.Entities.Product.Product
            {
                Title = Product.Title,
                Description = Product.Description,
                Content = Product.Content,
                Tags = Product.Tags,
                Image = userAvatar,
                IsDeleted = false,
                
            };

            _productService.CreateProduct(product);

            return RedirectToPage("Index");
        }

    }
}
