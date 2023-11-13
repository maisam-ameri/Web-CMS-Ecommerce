using Cms.Core.DTOs.UserPanel;
using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Web.Areas.Shop.Controllers
{
    [Area("shop")]
    public class HomeController : Controller
    {
        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        private int _pageId;

        public IActionResult Index(int pageId = 1, string keyword="",int minPrice=0,int maxPrice=int.MaxValue, List<int>? selectedCategories =null)
        {
            _pageId = pageId == 1 ? _pageId++ : _pageId--;

            var products = _productService.GetProductsForShop(pageId, keyword, minPrice,maxPrice,selectedCategories);
            
            ViewData["categories"] = _productService.GetCategories();
            ViewData["pageInfo"] = new Tuple<int,int>( _productService.GetTotalProductPageCount(),pageId);
            
            return View(products);

        }
    }
}
