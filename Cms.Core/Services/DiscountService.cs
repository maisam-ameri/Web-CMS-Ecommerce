using Cms.Core.DTOs.Shop;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities.Shop;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services
{
    public class DiscountService : IDiscountService
    {
        private CmsContext _context;


        public DiscountService(CmsContext context)
        {
            _context = context;
        }


        public IEnumerable<Discount> GetDiscounts()
        {
            return _context.Discounts;
        }

        public void CreateDiscount(DiscountDto discount)
        {
            var newdiscount = new Discount
            {
                Title = discount.Title,
                Amount = discount.Amount,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                    
            };

            var entity = _context.Discounts.Add(newdiscount);
            _context.SaveChanges();
            discount.DiscountId = entity.Entity.DiscountId;
            AssigDiscountToProducts(discount);
            _context.SaveChanges();
        }

        public Task AssigDiscountToProducts(DiscountDto discount)
        {
            List<int> productIdsToInt = new List<int>();
             discount.ProductIds.Split(',').ToList().ForEach(i => productIdsToInt.Add(int.Parse(i)));

            foreach (var productId in productIdsToInt)
            {
                var product = _context.Products.SingleOrDefault(p => p.ProductId == productId);
                product.DiscountId = discount.DiscountId;
                _context.Update(product);


            }
   
            return Task.FromResult(productIdsToInt);
        }
    }
}
