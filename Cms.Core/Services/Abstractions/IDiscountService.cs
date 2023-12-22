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

        #region Discount

        IEnumerable<Discount> GetDiscounts();
        Discount GetDiscount(int? discountId);
        public void CreateDiscount(DiscountDto discount);
        public int GetDiscountAmountByProductId(int productId);
        public void DeleteDiscount(int discountId);
        List<int>? GetDiscountProductIds(int discountId);
        void UpdateDiscount(Discount discount, string? productIds);

        #endregion

        #region DiscountCode
        IEnumerable<DiscountCode> GetDiscountCodes();
        public DiscountCode GetDiscountCode(int discountCodeId);
        public IEnumerable<DiscountCodeUser> GetDiscountCodesUserByUserId(int userId);
        public List<string> GetRolesIdsAssignedToDiscountCode(int discountCodeId);
        void CreateDiscountCode(DiscountCode discountCode);
        public void UpdateDiscountCode(DiscountCode discountCode);
        void DeleteDiscountCode(int discountCodeId);
        void AssignDiscountCodeToUsers(int discountCodeId, List<string> roleIds);


        #endregion
    }
}
