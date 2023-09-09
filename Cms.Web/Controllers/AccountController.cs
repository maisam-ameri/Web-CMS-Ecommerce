using Microsoft.AspNetCore.Mvc;

namespace Cms.Web.Controllers
{
    public class AccountController : Controller
    {
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
