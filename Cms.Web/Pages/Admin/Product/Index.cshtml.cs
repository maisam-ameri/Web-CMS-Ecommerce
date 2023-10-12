using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Product
{

    public class IndexModel : PageModel
    {

        private IProductService _productService;


        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public void OnGet()
        {
            var products = _productService.GetProducts();
            ViewData[nameof(products)] = products;
        }

    }
}
