using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services
{
    public class ProductService : IProductService
    {
        private CmsContext _context;

        public ProductService(CmsContext context)
        {
            _context = context;
        }
        public IEnumerable<CourseCategory> GetCategories() => _context.CourseCategories.ToList();
    }
}
