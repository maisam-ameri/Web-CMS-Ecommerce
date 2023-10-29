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
        private CmsContext _context;


        public CreateBaseContentModel(CmsContext context, IContentService contentService)
        {
            _context = context;
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

        public void CreateNewContent()
        {
            var content = new DataLayer.Entities.Content.BaseContent
            {
                ImageName = "",
                IsDeleted = false,
                IsPublished = false,
                MainText = "this is a new contentn",

                ShortDescription = "detail",
                Title = "Title",


            };
            _context.Add(content);
            _context.SaveChanges();
            
        }


    }
}
