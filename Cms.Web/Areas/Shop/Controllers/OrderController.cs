using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace Cms.Web.Areas.Shop.Controllers
{
    [Area("shop")]
    public class OrderController : Controller
    {
        private IUserService _userService;
        private IOrderService _orderService;

        public OrderController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }


        [Route("shop/BuyProduct/{id}")]
        public IActionResult AddOrderDetail(int id)
        {
            var userName= User.Identity.Name;
            var userId = _userService.GetUserByUserName(userName).Id;
            _orderService.AddOrderDetail(userId,id);
            return Content("Added");
        }

    }
}
