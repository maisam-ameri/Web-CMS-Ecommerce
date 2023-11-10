using Cms.Core.FileManager;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Web.Controllers
{
    public class GeneralController : Controller
    {
        private ImageManager _imageManager;
        public GeneralController(ImageManager imageManager)
        {
            _imageManager = imageManager;
        }

        [HttpPost]
        [Route("upload-file")]
        public IActionResult UploadImages(IFormFile upload)
        {
            //var imageName = upload.FileName;

            var imageName = _imageManager.UploadImage("", upload, "images/myImages/");
                var url = $"/images/myImages/{imageName}";

                return new JsonResult(new { uploaded = true, url });
        }


    }
}
