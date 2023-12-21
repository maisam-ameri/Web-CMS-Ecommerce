using Cms.Core.DTOs.AdminPanel.Product;
using Cms.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        IEnumerable<SelectListItem> GetProductsForDiscountCreation();
        IEnumerable<SelectListItem> GetProductsForDiscountEdition(int discountId);
        Product GetProduct(int? id);
        ProductDto GetProductForEditInAdmin(int? id);
        Cms.Core.DTOs.Shop.ShowProductDto GetProductForShow(int id);
        void CreateProduct(ProductDto product);
        void UpdateProduct(ProductDto product);
        void DeleteProduct(int id);

        #endregion

        #region Shop
        IEnumerable<Cms.Core.DTOs.Shop.ProductDto> GetLastProducts();
        List<Cms.Core.DTOs.Shop.ProductDto> GetProductsForShop(int pageId = 1, string keyword = "", int minPrice = 0, int maxPrice = int.MaxValue, List<int>? selectedCategories = null);

        int GetTotalProductPageCount();

        #endregion
    }
}
