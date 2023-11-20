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
        void AddOrderDetail(int userId, int productId);
        public Order GetOpenOrder(int userId);
        public List<OrderDetail> GetOrderDetailsInOpenOrder(int orderId);
        IEnumerable<UserOrderDto> GetOrdersForUser(int userId);
        Order GetOrder(int orderId);
    }
}
