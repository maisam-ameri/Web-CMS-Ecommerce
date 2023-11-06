using Cms.Core.DTOs.AdminPanel.Content;
using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities.Content;
using Hangfire;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Cms.Web.Pages.Admin.Content.BaseContent
{
    public class CreateBaseContentModel : PageModel
    {
        [BindProperty]
        public BaseContentDto Content { get; set; }

        private IContentService _contentService;


        public CreateBaseContentModel(IContentService contentService)
        {
            _contentService = contentService;
        }

        public void OnGet()
        {
            var categories = _contentService.GetCategories();
            ViewData["Categories"] = new SelectList(categories, "CategoryId", "Title"); 

        }

        public IActionResult OnPost(string categoryId)
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            Content.CategoryId = int.Parse(categoryId);
            _contentService.CreateBaseContent(Content);

            return RedirectToPage("Index");
        }

    }
}
