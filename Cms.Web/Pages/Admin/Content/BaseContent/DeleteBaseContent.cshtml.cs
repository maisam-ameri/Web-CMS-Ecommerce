using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Content.BaseContent
{
    public class DeleteBaseContentModel : PageModel
    {
        [BindProperty]
        public Cms.DataLayer.Entities.Content.BaseContent Content { get; set; }

        private IContentService _contentService;

        public DeleteBaseContentModel(IContentService contentService)
        {
            _contentService = contentService;
        }
        public IActionResult OnGet(int? id)
        {
            if (id == null) return Page();

            Content = _contentService.GetBaseContent(id.Value);

            return Page();
        }


        public IActionResult OnPost()
        {

            _contentService.DeleteBaseContent(Content.ContentId);
            return RedirectToPage("Index");
        }
    }
}
