using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Cms.Web.ViewComponents
{
    public class ProductCategoryComponent : ViewComponent
    {
        private IProductService _productService;

        public ProductCategoryComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _productService.GetCategories();
            return await Task.FromResult((IViewComponentResult) View("ProductCategory", categories));
        }

    }
}
