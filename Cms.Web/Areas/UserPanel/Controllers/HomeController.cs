using Cms.Core.DTOs;
using Cms.Core.DTOs.UserPanel;
using Cms.Core.FileManager;
using Cms.Core.Security;
using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Entities;
using Cms.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.Drawing.Imaging;
using System.Security.Policy;
using System.Text;

namespace Cms.Web.Areas.UserPanel.Controllers
{
    [Area("userPanel")]
    public class HomeController : Controller
    {

        private IUserService _userService;
        private IOrderService _orderService;
        private IWebHostEnvironment _webHostEnvironment;
        private ImageManager _imageManager;


        private const string AVATAR_PATH = "images/user/avatar/";

        public HomeController(IUserService userService, IWebHostEnvironment webHostEnvironment, IOrderService orderService, ImageManager imageManager)
        {
            _userService = userService;
            _orderService = orderService;
            _webHostEnvironment = webHostEnvironment;
            _imageManager = imageManager;
        }
        public IActionResult Index()
        {
            var user = _userService.GetUserInformation(User.Identity.Name);
            return View(user);
        }

        #region Edit Profile


        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            var userProfile = _userService.GetEditUserProfile(User.Identity.Name);
            return View(userProfile);
        }

        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditProfileDto profile)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }
            var user = _userService.GetUserByUserName(User.Identity.Name);

            if (_userService.IsExistUserName(profile.UserName) && profile.UserName != user.UserName)
            {
                ModelState.AddModelError("UserName", "نام کاربری وارد شده تکراری می باشد");
                return View(profile);
            }
            if (_userService.IsExistEmail(profile.Email) && profile.Email != user.Email)
            {
                ModelState.AddModelError("UserName", "ایمیل وارد شده تکراری می باشد");
                return View(profile);
            }

            var newAvatarName = _imageManager.UploadImage(profile.AvatarName, profile.Avatar, AVATAR_PATH);
            user.Avatar = newAvatarName;
            user.UserName = profile.UserName;
            user.Email = profile.Email;
            profile.AvatarName = newAvatarName;
            _userService.UpdateUser(user);

            // redirect to the login page

            return Redirect("/Logout?isProfileEdited=true");
        }

        #endregion

        #region Edit Password

        [Route("UserPanel/EditPassword")]
        public IActionResult EditPassword()
        {
            return View();
        }

        [Route("UserPanel/EditPassword")]
        [HttpPost]
        public IActionResult EditPassword(EditPasswordDto editPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(editPassword);
            }

            var hashPassword = PasswordHash.EncodePasswordMd5(editPassword.CurrentPassword);
            var isPasswordCorrect = _userService.CheckCurrentPassword(User.Identity.Name, hashPassword);



            if (isPasswordCorrect)
            {
                var user = _userService.GetUserByUserName(User.Identity.Name);
                user.Password = PasswordHash.EncodePasswordMd5(editPassword.NewPassword);
                _userService.UpdateUser(user);

                ViewBag.isSuccess = true;
                return View();
            }

            return View(editPassword);
        }
        #endregion

        #region Orders

        [Route("UserPanel/UserOrders")]
        public IActionResult UserOrders()
        {
            var userId = _userService.GetUserByUserName(User.Identity.Name).Id;
            var orders = _orderService.GetOrdersForUser(userId);
            return View(orders);
        }


        [Route("UserPanel/UserOrderDetails/{orderId}")]
        public IActionResult UserOrderDetails(int orderId)
        {
            var orderDetails = _orderService.GetOrderDetailsInOpenOrder(orderId);
            ViewData["isOrderFinally"] = _orderService.GetOrder(orderId).IsFinally ? true : null;
            return View(orderDetails);
        }


        [Route("UserPanel/DeleteOrder/{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            return PartialView("_DeleteOrder", orderId);
        }

        [HttpPost]
        [Route("UserPanel/DeleteOrder/{orderId}")]
        public IActionResult DeleteOrderPost(int orderId)
        {
            _orderService.DeleteOrder(orderId);
            return RedirectToAction(nameof(UserOrders));
        }



        [Route("UserPanel/DeleteOrderDetail/{orderDetailId}")]
        public IActionResult DeleteOrderDetail(int orderDetailId)
        {
            var userId = _userService.GetCurrentUserIdByUserName(User.Identity.Name);

            var orderDetail = _orderService.GetOrderDetailInOpenOrder(userId, orderDetailId);

            var data = Tuple.Create(orderDetail.OrderDetailId, orderDetail.Title);

            return PartialView("_DeleteOrderDetail", data);
        }

        [HttpPost]
        [Route("UserPanel/DeleteOrderDetail/{orderDetailId}")]
        public IActionResult DeleteOrderDetailPost(int orderDetailId)
        {
            var userId = _userService.GetCurrentUserIdByUserName(User.Identity.Name);
            var orderId = _orderService.DeleteOrderDetail(userId, orderDetailId);

            if (orderId == null)
            {
                return RedirectToAction(nameof(UserOrders));
            }

            return RedirectToAction(nameof(UserOrderDetails), new { orderId });



        }

        #endregion

    }
}
