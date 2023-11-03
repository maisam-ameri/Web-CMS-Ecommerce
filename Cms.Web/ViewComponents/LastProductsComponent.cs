using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Web.ViewComponents
{
    public class LastProductsComponent : ViewComponent
    {
        private IProductService _ProductService;


        public LastProductsComponent(IProductService productService)
        {
            _ProductService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = _ProductService.GetLastProducts();
            return await Task.FromResult((IViewComponentResult) View("LastProducts", products));

            
        }
    }
}
