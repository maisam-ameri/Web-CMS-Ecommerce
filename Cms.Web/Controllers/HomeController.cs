using Microsoft.AspNetCore.Mvc;

namespace Cms.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
