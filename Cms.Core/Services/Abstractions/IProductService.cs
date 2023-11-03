using Cms.Core.DTOs.AdminPanel.Product;

using Cms.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        IEnumerable<CategoryDto> GetCategoryDtos();
        IEnumerable<SelectListItem> GetCategories(int? parentId = null);
        List<CategoryDto> GetParentCategoriesForAdminPanel();

        int CreateCategory(Category category);
        Category GetCategory(int id);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);

        #endregion

        #region Product

        IEnumerable<Product> GetProducts();
        Product GetProduct(int? id);
        ProductDto GetProductForEditInAdmin(int? id);
        void CreateProduct(ProductDto product);
        void UpdateProduct(ProductDto product);
        void DeleteProduct(int id);

        #endregion

        #region Shop
        IEnumerable<Cms.Core.DTOs.Shop.ProductDto> GetLastProducts();

        #endregion
    }
}
