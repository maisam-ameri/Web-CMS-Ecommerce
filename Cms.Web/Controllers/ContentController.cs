using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Web.Controllers
{
    public class ContentController : Controller
    {
        private IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[Route("Content/showContent/{id}")]
        public IActionResult ShowContent(int id)
        {
            var content = _contentService.GetBaseContent(id);
            return View(content);
        }
    }
}
