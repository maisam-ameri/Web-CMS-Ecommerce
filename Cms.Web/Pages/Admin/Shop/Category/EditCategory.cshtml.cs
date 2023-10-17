using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Shop.Category
{
    public class EditCategoryModel : PageModel
    {
        [BindProperty]
        public Cms.DataLayer.Entities.Product.Category Category { get; set; }

        private IProductService _productService;


        public EditCategoryModel(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult OnGet(int id)
        {
            var categories = _productService.GetParentCategoriesForAdminPanel();
            ViewData[nameof(categories)] = categories;
            Category = _productService.GetCategory(id);
            return Page();
        }

        public IActionResult OnPost(string? parentCategoryId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(!string.IsNullOrEmpty(parentCategoryId))
            {
                Category.ParentId = int.Parse(parentCategoryId);

            }

            _productService.UpdateCategory(Category);


            return RedirectToPage("Index");
        }
    }
}
