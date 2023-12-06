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
        public Discount GetDiscount(int discountId)
        {
            return _context.Discounts.SingleOrDefault(d => d.DiscountId == discountId);
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

            var productIds = GetSplitedProductIds(discount.ProductIds);
            ExecuteActionOnProducts(productIds, (product) =>
            {
                if (product != null)
                {
                    product.DiscountId = discount.DiscountId;
                    _context.Update(product);
                }
            });

            _context.SaveChanges();
        }

        private Task ExecuteActionOnProducts(List<int>? productIds, Action<Product> action)
        {
            foreach (var productId in productIds)
            {
                var product = _context.Products.SingleOrDefault(p => p.ProductId == productId);
                action(product);
            }

            return Task.FromResult(productIds);
        }
        private List<int>? GetSplitedProductIds(string? productIds)
        {
            var productIdsToInt = new List<int>();
            productIds.Split(',').ToList().ForEach(i => productIdsToInt.Add(int.Parse(i)));
            return productIdsToInt;
        }

        public void DeleteDiscount(int discountId)
        {
            List<int>? products = _context.Products.Where(p => p.DiscountId == discountId).Select(p => p.ProductId).ToList();
            //var productIds = GetSplitedProductIds(discountId);
            ExecuteActionOnProducts(products, (product) =>
            {
                product.DiscountId = null;
                _context.Update(product);
            });

            var discountForDelete = _context.Discounts.SingleOrDefault(d => d.DiscountId == discountId);

            if(discountForDelete!= null)
                _context.Discounts.Remove(discountForDelete);

            _context.SaveChanges();
        }

    }
}
