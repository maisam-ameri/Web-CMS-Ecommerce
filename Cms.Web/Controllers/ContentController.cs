using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities.Content;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NuGet.Protocol;

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
            ViewData["comments"] = _contentService.GetComments(id);
            return View(content);   
        }

        [HttpPost]
        public IActionResult AddComment(int id)
        {
            var name = HttpContext.Request.Form["name"].FirstOrDefault();
            var email = HttpContext.Request.Form["email"].FirstOrDefault();
            var comment = HttpContext.Request.Form["comment"].FirstOrDefault(); 
            var data = new { name, email, comment };
            var json = Json(new {status = "ok", data});

            var newComment = new ContentComment
            {
                Name = name,
                Email = email,
                Comment = comment,
                ContentId = id,
                CreateDate = DateTime.Now,
            };

            _contentService.CreateContentComment(newComment);

            return GetComments(id);
        }

        public IActionResult GetComments(int id)    
        {
            var comments = _contentService.GetComments(id);

            return PartialView("_ShowComment", comments);
        }
    }
}
