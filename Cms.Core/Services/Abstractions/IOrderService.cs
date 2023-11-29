using Cms.Core.DTOs.UserPanel;
using Cms.DataLayer.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services.Abstractions
{
    public interface IOrderService
    {
        #region Order
        public Order GetOpenOrder(int userId);
        Order GetOrder(int orderId);
        IEnumerable<UserOrderDto> GetOrdersForUser(int userId);
        void DeleteOrder(int orderId);
        public int? GetOrderIdByOrderDetailId(int orderdetailId);
        public void UpdateOrder(Order order);
        #endregion

        #region OrderDetail
        void AddOrderDetail(int userId, int productId);
        OrderDetail? GetOrderDetailInOpenOrder(int userId, int orderDetailId);
        public List<OrderDetail> GetOrderDetailsInOpenOrder(int orderId);
        public int? DeleteOrderDetail(int userId,int orderDetailId);


        #endregion

    }
}
