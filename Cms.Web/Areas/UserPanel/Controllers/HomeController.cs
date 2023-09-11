using Microsoft.AspNetCore.Mvc;

namespace Cms.Web.Areas.UserPanel.Controllers
{
    [Area("userPanel")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }


    }
}
