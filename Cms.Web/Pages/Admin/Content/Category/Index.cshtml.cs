using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cms.Web.Pages.Admin.Content
{
    public class IndexModel : PageModel
    {
        private IContentService _contentService { get; set; }

        public IndexModel(IContentService contentService)
        {
            _contentService = contentService;
        }
        public List<Role> Roles { get; set; }
        public void OnGet()
        {

            ViewData["Categories"] = _contentService.GetCategories();

        }
    }
}
