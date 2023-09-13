using Cms.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Web.Areas.UserPanel.Controllers
{
    [Area("userPanel")]
    public class HomeController : Controller
    {

        private IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var user = _userService.GetUserInformation(User.Identity.Name);
            return View(user);
        }

        public IActionResult Details()
        {
            return View();
        }


    }
}
