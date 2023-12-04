using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities.Shop;
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
    }
}
