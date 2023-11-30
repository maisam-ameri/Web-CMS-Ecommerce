using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ZarinpalSandbox;

namespace Cms.Web.Areas.UserPanel.Controllers
{
    public class PaymentController : Controller
    {
        private IOrderService _orderService;
        private IUserService _userService;

        private const string PAYMENT_REQUEST_CALLBACK_URL = "http://localhost:5186/userPanel/RequestCallback";
        private string ZARIN_URL => "https://sandbox.zarinpal.com/pg/StartPay";




        public PaymentController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }


        [Route("Payment/PayOrder/{orderId}")]
        public IActionResult PayOrder(int orderId)
        {
            var totalPrice = _orderService.GetOrderDetailsInOpenOrder(orderId).Sum(o => o.Price);

            var payment = new Payment(totalPrice);
            var response = payment.PaymentRequest($"پرداخت فاکتور {orderId}", PAYMENT_REQUEST_CALLBACK_URL);

            if (response.Result.Status == 100)
            {
                return Redirect($"{ZARIN_URL}/{response.Result.Authority}");
            }

            return RedirectToAction(nameof(PaymentFailed));
        }

        [Route("userPanel/RequestCallback")]
        public IActionResult Callback()
        {
            var authority = HttpContext.Request.Query["Authority"];
            var status = HttpContext.Request.Query["Status"].ToString();
            var userId = _userService.GetCurrentUserIdByUserName(User.Identity.Name);
            var orderId = _orderService.GetOpenOrder(userId).OrderId;

            if (status.ToLower() == "ok")
            {

                var result = VerifyPayment(orderId, authority.ToString());

                if (result.HasValue)
                {
                    return PaymentSuccessed(orderId, result.Value);
                }
                else
                {
                    return PaymentFailed();

                }
            }
            else
            {
                return PaymentFailed();

            }

        }

        public long? VerifyPayment(int orderId, string authority)
        {

            var totalPrice = _orderService.GetOrderDetailsInOpenOrder(orderId).Sum(o => o.Price);

            var payment = new Payment(totalPrice);
            var result = payment.Verification(authority);

            if (result.Result.Status == 100)
            {
                return result.Result.RefId;
            }
            else
            {
                return null;
            }
        }


        [Route("UserPanel/PaymentFailed")]
        public IActionResult PaymentFailed()
        {
            var userId = _userService.GetCurrentUserIdByUserName(User.Identity.Name);
            var orderId = _orderService.GetOpenOrder(userId).OrderId;
            var orders = _orderService.GetOrdersForUser(userId);

            var isPaymnetSuccessed = false;
            long refId = 0;
            var paymentData = Tuple.Create(orderId, isPaymnetSuccessed, refId);
            ViewData["paymentData"] = paymentData;
            return View("~/Areas/UserPanel/Views/Home/UserOrders.cshtml", orders);

        }

        public IActionResult PaymentSuccessed(int orderId, long refId)
        {

            var order = _orderService.GetOrder(orderId);
            order.IsFinally = true;
            _orderService.UpdateOrder(order);

            var userId = _userService.GetCurrentUserIdByUserName(User.Identity.Name);
            var orders = _orderService.GetOrdersForUser(userId);

            var isPaymnetSuccessed = true;
            var paymentData = Tuple.Create(orderId, isPaymnetSuccessed, refId);

            ViewData["paymentData"] = paymentData;
            return View("~/Areas/UserPanel/Views/Home/UserOrders.cshtml", orders);
        }
    }
}
