using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Content.Category
{
    public class EditContentCategoryModel : PageModel
    {
        [BindProperty]
        public Cms.DataLayer.Entities.Content.Category Category { get; set; }

        private IContentService _contentService;


        public EditContentCategoryModel(IContentService contentService)
        {
            _contentService = contentService;
        }

        public void OnGet(int id)
        {
            Category = _contentService.GetCategory(id);
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _contentService.UpdateContentCategory(Category);

            return RedirectToPage("Index");
        }
    }
}
