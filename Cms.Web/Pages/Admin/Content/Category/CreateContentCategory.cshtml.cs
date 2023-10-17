using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cms.Web.Pages.Admin.Content.Category
{
    public class CreateContentCategoryModel : PageModel
    {
        [BindProperty]
        public Cms.DataLayer.Entities.Content.Category Category { get; set; }


        private IContentService _contentService;



        public CreateContentCategoryModel(IContentService contentService)
        {
            _contentService = contentService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }

            var newCategory = new Cms.DataLayer.Entities.Content.Category()
            {
                Title = Category.Title,
                IsDeleted = false
            };

            _contentService.CreateContentCategory(newCategory);

            return RedirectToPage("Index");

        }
    }
}
