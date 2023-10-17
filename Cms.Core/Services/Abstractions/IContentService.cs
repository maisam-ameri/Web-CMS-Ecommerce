using Cms.DataLayer.Entities.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services.Abstractions
{
    public interface IContentService
    {
        List<Category> GetCategories();
        Category GetCategory(int categoryId);
    }
}
