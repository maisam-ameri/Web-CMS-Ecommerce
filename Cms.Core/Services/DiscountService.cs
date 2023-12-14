using Cms.Core.DTOs.Shop;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities.Shop;
using Hangfire;
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

        #region Discount

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
            var compareNowWithStartDate = DateTime.Now.CompareTo(discount.StartDate);
            var newdiscount = new Discount
            {
                Title = discount.Title,
                Amount = discount.Amount,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                IsActive = (compareNowWithStartDate == 0 || compareNowWithStartDate == 1) ? true : false

            };

            var entity = _context.Discounts.Add(newdiscount);
            _context.SaveChanges();
            discount.DiscountId = entity.Entity.DiscountId;
            UpdateDiscountInProduct(newdiscount, discount.ProductIds);

            _context.SaveChanges();

            if (!newdiscount.IsActive)
            {
                newdiscount.JobId = BackgroundJob.Schedule(() => ActiveDiscountJob(discount.DiscountId), discount.StartDate);
            }
        }
        public void ActiveDiscountJob(int discountId)
        {
            var discount = GetDiscount(discountId);
            if (discount != null)
            {
                discount.IsActive = true;
                discount.JobId = null;
                _context.Update(discount);
                _context.SaveChanges();
            }
        }
        public void DeleteDiscount(int discountId)
        {
            var discount = GetDiscount(discountId);

            RemoveDiscountOfProduct(discountId);

            var discountForDelete = _context.Discounts.SingleOrDefault(d => d.DiscountId == discountId);

            if (discountForDelete != null)
                _context.Discounts.Remove(discountForDelete);

            if (!string.IsNullOrEmpty(discount.JobId))
                BackgroundJob.Delete(discount.JobId);


            _context.SaveChanges();
        }

        private void RemoveDiscountOfProduct(int discountId)
        {
            List<int>? products = _context.Products.Where(p => p.DiscountId == discountId).Select(p => p.ProductId).ToList();
            //var productIds = GetSplitedProductIds(discountId);
            ExecuteActionOnProducts(products, (product) =>
            {
                product.DiscountId = null;
                _context.Update(product);
            });
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
        private void UpdateDiscountInProduct(Discount discount, string? productIds)
        {
            var productIdsToInt = GetSplitedProductIds(productIds);
            ExecuteActionOnProducts(productIdsToInt, (product) =>
            {
                if (product != null)
                {
                    product.DiscountId = discount.DiscountId;
                    _context.Update(product);
                }
            });
        }
        public void UpdateDiscount(Discount discount, string? productIds)
        {
            if (discount == null) return;

            RemoveDiscountOfProduct(discount.DiscountId);
            UpdateDiscountInProduct(discount, productIds);

            var compareNowWithStartDate = DateTime.Now.CompareTo(discount.StartDate);
            var IsActive = (compareNowWithStartDate == 0 || compareNowWithStartDate == 1) ? true : false;
            discount.IsActive = IsActive;

            if (!string.IsNullOrEmpty(discount.JobId))
                BackgroundJob.Delete(discount.JobId);

            if (!discount.IsActive)
            {
                discount.JobId = BackgroundJob.Schedule(() => ActiveDiscountJob(discount.DiscountId), discount.StartDate);
            }
            else
            {
                discount.JobId = null;
            }

            _context.Update(discount);
            _context.SaveChanges();
        }
        public List<int>? GetDiscountProductIds(int discountId)
        {
            return _context.Products.Where(p => p.DiscountId == discountId).Select(d => d.ProductId).ToList();
        }


        #endregion


        #region DiscountCode
        public IEnumerable<DiscountCode> GetDiscountCodes()
        {
            return _context.DiscountCodes.ToList();
        }
        public DiscountCode GetDiscountCode(int discountCodeId)
        {
            return _context.DiscountCodes.SingleOrDefault(d => d.DiscountCodeId == discountCodeId);
        }

        public void CreateDiscountCode(DiscountCode discountCode)
        {
            if (discountCode == null) return;

            var compareNowWithStartDate = DateTime.Now.CompareTo(discountCode.StartDate);

            var newdiscountCode = new DiscountCode
            {
                Title = discountCode.Title,
                Amount = discountCode.Amount,
                StartDate = discountCode.StartDate,
                EndDate = discountCode.EndDate,
                IsActive = (compareNowWithStartDate == 0 || compareNowWithStartDate == 1) ? true : false

            };

            var entity = _context.Add(newdiscountCode);
            _context.SaveChanges();

            if (!newdiscountCode.IsActive)
            {
                newdiscountCode.JobId = BackgroundJob.Schedule(() => ActiveDiscountCodeJob(newdiscountCode.DiscountCodeId), newdiscountCode.StartDate);

                _context.Update(newdiscountCode);
                _context.SaveChanges();

            }

        }
        public void ActiveDiscountCodeJob(int discountId)
        {
            var discountCode = GetDiscountCode(discountId);
            if (discountCode != null)
            {
                discountCode.IsActive = true;
                discountCode.JobId = null;
                _context.Update(discountCode);
                _context.SaveChanges();
            }
        }

        public void DeleteDiscountCode(int discountCodeId)
        {
            var discountForDelete = _context.DiscountCodes.SingleOrDefault(d => d.DiscountCodeId == discountCodeId);

            if (discountForDelete != null)
            {

                if (!string.IsNullOrEmpty(discountForDelete.JobId))
                    BackgroundJob.Delete(discountForDelete.JobId);

                _context.DiscountCodes.Remove(discountForDelete);

                _context.SaveChanges();
            }
        }
        public void UpdateDiscountCode(DiscountCode discountCode)
        {
            if (discountCode == null) return;

            var compareNowWithStartDate = DateTime.Now.CompareTo(discountCode.StartDate);
            var IsActive = (compareNowWithStartDate == 0 || compareNowWithStartDate == 1) ? true : false;
            discountCode.IsActive = IsActive;

            if (!string.IsNullOrEmpty(discountCode.JobId))
                BackgroundJob.Delete(discountCode.JobId);

            if (!discountCode.IsActive)
            {
                discountCode.JobId = BackgroundJob.Schedule(() => ActiveDiscountJob(discountCode.DiscountCodeId), discountCode.StartDate);
            }
            else
            {
                discountCode.JobId = null;
            }

            _context.Update(discountCode);
            _context.SaveChanges();
        }



        #endregion
    }
}
