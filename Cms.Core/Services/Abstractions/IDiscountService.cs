using Cms.Core.DTOs.Shop;
using Cms.DataLayer.Entities.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services.Abstractions
{
    public interface IDiscountService
    {

        IEnumerable<Discount> GetDiscounts();
        Discount GetDiscount(int discountId);

        public void CreateDiscount(DiscountDto discount);
        public void DeleteDiscount(int discountId);
    }
}
