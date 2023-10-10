using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Category
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
            var categories = _productService.GetCategories();
            ViewData[nameof(categories)] = categories;
        }
    }
}
