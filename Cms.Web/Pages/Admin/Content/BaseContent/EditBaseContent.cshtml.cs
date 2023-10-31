using Cms.Core.DTOs.AdminPanel.Content;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cms.Web.Pages.Admin.Content.BaseContent
{
    public class EditBaseContentModel : PageModel
    {
        [BindProperty]
        public BaseContentDto Content { get; set; }

        private IContentService _contentService;


        public EditBaseContentModel(IContentService contentService, ImageManager imageManager)
        {
            _contentService = contentService;

        }

        public void OnGet(int id)
        {
            var categories = _contentService.GetContentCategoryDtos();
            ViewData["categories"] = new SelectList(categories, "Id", "Title");
            Content = _contentService.GetContentForEditInAdmin(id);
        }

        public IActionResult OnPost(string? categoryId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Content.CategoryId = int.Parse(categoryId);

            _contentService.UpdateBaseContent(Content);

            return RedirectToPage("Index");
        }
    }
}
