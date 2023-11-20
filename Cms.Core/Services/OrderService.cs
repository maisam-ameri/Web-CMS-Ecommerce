using Cms.Core.DTOs.UserPanel;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
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

            // add orderDetail
            OrderDetail newOrderDetail;

            if (order != null)
            {

                newOrderDetail = GetOrderDetailInOpenOrder(order.OrderId, productId);



                if (newOrderDetail == null)
                {
                    newOrderDetail = new OrderDetail()
                    {
                        ProductId = productId,
                        Title = product.Title,
                        Count = 1,
                        RegisterDate = DateTime.Now,
                        Price = product.Price,
                        OrderId = order.OrderId,
                    };
                    order.OrderSum += newOrderDetail.Price;

                    _context.Add(newOrderDetail);
                    _context.SaveChanges();
                    _context.Update(order);
                }

                else
                {
                    newOrderDetail.Count++;
                    order.OrderSum += newOrderDetail.Price;
                 
                    _context.Update(order);
                    _context.SaveChanges();

                }
            }
            else
            {
                order = CreateOrder(userId);
                order.OrderSum = product.Price;
                newOrderDetail = new OrderDetail()
                {
                    ProductId = productId,
                    Title= product.Title,
                    Count = 1,
                    RegisterDate = DateTime.Now,
                    Price = product.Price,
                    OrderId = order.OrderId,

                };
                
                _context.Add(newOrderDetail);
                _context.SaveChanges();
            }
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.SingleOrDefault(o => o.OrderId == orderId);
        }

        public List<OrderDetail> GetOrderDetailsInOpenOrder(int orderId)
        {

            return _context.OrderDetails.Where(o => o.OrderId == orderId).ToList();
        }

        public OrderDetail GetOrderDetailInOpenOrder(int orderId,int productId)
        {
         
            return _context.OrderDetails.SingleOrDefault(o => o.OrderId == orderId && o.ProductId == productId);
        }

        public Order CreateOrder(int userId)
        {
            var newOrder = new Order
            {
                UserId = userId,
                RegisterDate = DateTime.Now,
                IsFinally = false,
            };

            var order = _context.Add(newOrder);
            _context.SaveChanges();

            return order.Entity;
        }

        public Order GetOpenOrder(int userId) => _context.Orders.SingleOrDefault(o => o.UserId == userId && o.IsFinally == false);


        public IEnumerable<UserOrderDto> GetOrdersForUser(int userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).OrderBy(o => o.RegisterDate).Select(u => new UserOrderDto()
            {
                RegisterDate = u.RegisterDate,
                IsFinally = u.IsFinally,
                OrderId = u.OrderId,
                DetailOrderCount = u.OrderDetails.Count()
            });
        }
    }
}
