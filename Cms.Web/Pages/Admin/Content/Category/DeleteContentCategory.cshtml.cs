using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Content.Category
{
    public class DeleteContentCategoryModel : PageModel
    {
        [BindProperty]
        public Cms.DataLayer.Entities.Content.Category Category { get; set; }

        private IContentService _contentService;

        public DeleteContentCategoryModel(IContentService contentService)
        {
            _contentService = contentService;
        }
        public IActionResult OnGet(int? id)
        {
            if (id == null) return Page();

            Category = _contentService.GetCategory(id.Value);

            return Page();
        }


        public IActionResult OnPost()
        {

            _contentService.DeleteCategory(Category.CategoryId);
            return RedirectToPage("Index");
        }
    }
}
