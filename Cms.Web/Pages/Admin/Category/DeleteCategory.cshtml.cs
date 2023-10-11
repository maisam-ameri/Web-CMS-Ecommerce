using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities;
using Cms.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Product
{
    public class DeleteCategoryModel : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }

        private IProductService _productService;

        public DeleteCategoryModel(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult OnGet(int? id)
        {
            if (id == null) return Page();

            Category = _productService.GetCategory(id.Value);

            return Page();
        }


        public IActionResult OnPost()
        {

            _productService.DeleteCategory(Category.Id);
            return RedirectToPage("Index");
        }
    }
}
