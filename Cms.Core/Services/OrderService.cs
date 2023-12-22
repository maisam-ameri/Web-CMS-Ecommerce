using Cms.Core.DTOs.UserPanel;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities;
using Cms.DataLayer.Entities.Shop;


namespace Cms.Core.Services
{
    public class OrderService : IOrderService
    {
        private CmsContext _context;
        private IProductService _productService;

        public OrderService(CmsContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        /// <summary>
        /// check whether the user has any order that is finally
        /// if yes: create new order ,so assign its id to the orderDetail
        /// if no: get the order ,so assign its id to the orderDetail
        /// </summary>
        public void AddOrderDetail(int userId, int productId)
        {
            var product = _productService.GetProduct(productId);
            var order = GetOpenOrder(userId);


            if (order == null)
            {

                order = new Order
                {
                    IsFinally = false,
                    OrderSum = product.Price,
                    RegisterDate = DateTime.Now,
                    UserId = userId,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail
                        {
                            Price = product.Price,
                            ProductId = productId,
                            RegisterDate = DateTime.Now,
                            Title = product.Title,
                            Count = 1,
                        }
                    }
                };
                _context.Add(order);
            }
            else
            {
                var orderDetail = _context.OrderDetails.FirstOrDefault(o => o.ProductId == productId && o.OrderId == order.OrderId);

                if (orderDetail != null)
                {
                    orderDetail.Count++;
                    order.OrderDetails.Add(orderDetail);

                }
                else
                {
                    _context.OrderDetails.Add(new OrderDetail
                    {
                        OrderId = order.OrderId,
                        Price = product.Price,
                        ProductId = productId,
                        RegisterDate = DateTime.Now,
                        Title = product.Title,
                        Count = 1,
                    });
                }
                order.OrderSum += product.Price;
                _context.Update(order);

            }
            _context.SaveChanges();
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.SingleOrDefault(o => o.OrderId == orderId);
        }
        public int? GetOrderIdByOrderDetailId(int orderdetailId)
        {
            var orderDetail = _context.OrderDetails.FirstOrDefault(o => o.OrderDetailId == orderdetailId);
            if (orderDetail != null) return orderDetail.OrderId;
            return null;
        }

        public List<OrderDetail> GetOrderDetailsInOpenOrder(int orderId)
        {

            return _context.OrderDetails.Where(o => o.OrderId == orderId).ToList();
        }
        public OrderDetail GetOrderDetailByProductId(int productId)
        {
            return _context.OrderDetails.SingleOrDefault(o => o.ProductId == productId);
        }

        public Order GetOpenOrder(int userId) => _context.Orders.SingleOrDefault(o => o.UserId == userId && o.IsFinally == false);

        public IEnumerable<UserOrderDto> GetOrdersForUser(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).OrderByDescending(o => o.RegisterDate).Select(u => new UserOrderDto()
            {
                RegisterDate = u.RegisterDate,
                IsFinally = u.IsFinally,
                OrderId = u.OrderId,
                DetailOrderCount = u.OrderDetails.Count()
            });
        }

        public void DeleteOrder(int orderId)
        {
            _context.OrderDetails.ToList().RemoveAll(o => o.OrderId == orderId);

            var order = _context.Orders.SingleOrDefault(o => o.OrderId == orderId);

            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            _context.SaveChanges();
        }

        public int? DeleteOrderDetail(int userId,int orderDetailId)
        {
            var order = GetOpenOrder(userId);
            var orderDetail = _context.OrderDetails.FirstOrDefault(o => o.OrderDetailId == orderDetailId && o .OrderId == order.OrderId);

            if (orderDetail != null)
            {
                if (orderDetail.Count > 1)
                {
                    orderDetail.Count--;
                    _context.Update(orderDetail);
                }
                else
                {
                    // if the order dosnt have any orderDetails , so delete the order
                    if (_context.OrderDetails.Where(o => o.OrderId == order.OrderId).Count() > 1)
                    {
                        _context.Remove(orderDetail);
                    }
                    else
                    {
                        _context.Remove(orderDetail);
                        DeleteOrder(order.OrderId);
                        return null;
                    }
                }
                _context.SaveChanges();
                return order.OrderId;

            }

            return null;


        }

        public OrderDetail? GetOrderDetailInOpenOrder(int userId, int orderDetailId)
        {
            var order = GetOpenOrder(userId);
            if (order != null)
            {
                var orderDetail = _context.OrderDetails.FirstOrDefault(o => o.OrderId == order.OrderId && o.OrderDetailId == orderDetailId);
                
                if (orderDetail != null) return orderDetail;
            }

            return null;
        }

        public void UpdateOrder(Order order)
        {
            if (order == null) return;
            _context.Update(order);
            _context.SaveChanges();
        }
    }
}
