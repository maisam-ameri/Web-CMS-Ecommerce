using Cms.Core.DTOs.AdminPanel.Product;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;

namespace Cms.Web.Pages.Admin.Shop.Product
{
    public class CreateProductModel : PageModel
    {
        [BindProperty]
        public ProductDto Product { get; set; }

        private IProductService _productService;
        private ImageManager _imageManager { get; set; }
        private const string PRODUCT_PATH = "images/product/";

        //private readonly IAntiforgery _antiForgery;
        //public AntiforgeryTokenSet AntiForgeryToken { get; set; }


        public CreateProductModel(IProductService productService, ImageManager imageManager, IAntiforgery antiforgery)
        {
            _productService = productService;
            _imageManager = imageManager;
            //  _antiForgery = antiforgery;
        }

        public void OnGet()
        {
            var categories = _productService.GetCategories(null);
            ViewData[nameof(categories)] = new SelectList(categories, "Value", "Text");

            //AntiForgeryToken = _antiForgery.GetAndStoreTokens(HttpContext);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var categoryId = Request.Form["categoryId"];
            var productImageName = _imageManager.UploadImage("", Product.Image, PRODUCT_PATH);

            Product.ImageName = productImageName;
            Product.CategoryId = int.Parse(categoryId);



            _productService.CreateProduct(Product);

            return RedirectToPage("Index");
        }

        public IActionResult OnGetSubCategories(string categoryId)
        {
            var subCategories = _productService.GetCategories(int.Parse(categoryId));

            return new JsonResult(subCategories);
        }

    }
}
