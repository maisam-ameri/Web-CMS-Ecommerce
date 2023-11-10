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

        public IActionResult Index()
        {
            var products = _productService.GetProducts();
            return View();
        }
    }
}
