using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace Cms.Web.Areas.Shop.Controllers
{
    [Area("shop")]
    public class OrderController : Controller
    {
        [Route("/shop/AddOrder/{id}")]
        public IActionResult Index(int id)
        {
            return Content("order: " + id);
        }
    }
}
