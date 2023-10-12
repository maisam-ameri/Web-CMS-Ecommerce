using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cms.Web.Pages.Admin.Category
{
    public class CreateCategoryModel : PageModel
    {

        [BindProperty]
        public Cms.DataLayer.Entities.Product.Category Category { get; set; }

        [BindProperty]
        public List<SelectListItem>? ParentId { get; set; }

        private IProductService _productService;



        public CreateCategoryModel(IProductService productService)
        {
            _productService = productService;
        }
        public void OnGet()
        {
            var categories = _productService.GetParentCategoriesForAdminPanel();
            ViewData[nameof(categories)] = categories;
        }

        public IActionResult OnPost(string? parentCategoryId)
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }

            int? parentId = null;

            if (!string.IsNullOrEmpty(parentCategoryId))
                parentId = int.Parse(parentCategoryId);
     

            var category = new Cms.DataLayer.Entities.Product.Category
            {
                ParentId = parentId,
                IsDeleted = false,
                Title = Category.Title
            };

            _productService.CreateCategory(category);
            return RedirectToPage("Index");

        }
    }
}
