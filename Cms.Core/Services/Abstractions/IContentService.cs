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
        int CreateContentCategory(Category newCategory);
        void DeleteCategory(int id);
        List<Category> GetCategories();
        Category GetCategory(int categoryId);
        void UpdateContentCategory(Category category);
    }
}
