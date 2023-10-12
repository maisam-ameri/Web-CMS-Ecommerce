using Cms.Core.DTOs.AdminPanel.Product;
using Cms.DataLayer.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services.Abstractions
{
    public interface IProductService
    {
        #region Category

        IEnumerable<Category> GetCategories();
        List<CategoryDto> GetParentCategoriesForAdminPanel();

        int CreateCategory(Category category);
        Category GetCategory(int id);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);

        #endregion

        #region Product

        IEnumerable<Product> GetProducts();
        void CreateProduct(Product product);

        #endregion
    }
}
