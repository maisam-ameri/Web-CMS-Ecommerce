using Cms.DataLayer.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services.Abstractions
{
    public interface IProductService
    {
        IEnumerable<ProductCategory> GetCategories();
    }
}
