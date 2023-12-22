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


        [Route("shop/AddOrderDetail/{id}")]
        public IActionResult AddOrderDetail(int id)
        {
            var userName= User.Identity.Name;
            var userId = _userService.GetCurrentUserIdByUserName(userName);
            _orderService.AddOrderDetail(userId,id);
            var orderDetail =_orderService.GetOrderDetailByProductId(userId, id);
            return PartialView("_AddOrderDetailToCard", orderDetail);
        }

    }
}
