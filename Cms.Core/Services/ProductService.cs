using Cms.Core.DTOs.AdminPanel.Product;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cms.Core.Services
{
    public class ProductService : IProductService
    {
        private CmsContext _context;
        private ImageManager _imageManager { get; set; }
        private const string PRODUCT_PATH = "images/product/";

        public ProductService(CmsContext context, ImageManager imageManager)
        {
            _context = context;
            _imageManager = imageManager;
        }

        #region Category

        public int CreateCategory(Category category)
        {
            var newCat = _context.Categories.Add(category);

            _context.SaveChanges();
            return newCat.Entity.Id;

        }

        public IEnumerable<Category> GetCategories() => _context.Categories.ToList();

        public IEnumerable<CategoryDto> GetCategoryDtos()=> _context.Categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Title = c.Title,
        }).ToList();

        public Category GetCategory(int id)
        {
            return _context.Categories.SingleOrDefault(c => c.Id == id);
        }

        public List<CategoryDto> GetParentCategoriesForAdminPanel()
        {
            return _context.Categories.Where(c => c.ParentId == null).Select(c => new CategoryDto
            {
                Id = c.Id,
                Title = c.Title,
            }).ToList();
        }

        public void UpdateCategory(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = GetCategory(id);
            if (category != null)
            {
                category.IsDeleted = true;
                _context.SaveChanges();
            }



        }


        #endregion

        #region Product



        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProduct(int? id)
        {
            return _context.Products.Find(id);
        }


        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public ProductDto GetProductForEditInAdmin(int? id)
        {
            return _context.Products.Where(p => p.ProductId == id.Value).Select(p => new ProductDto
            {
                Content = p.Content,
                ProductId = id.Value,
                Description = p.Description,
                ImageName = p.Image,
                Title = p.Title,
                Tags = p.Tags,
                CategoryId = p.CategoryId,
            }).Single();
        }

        public void UpdateProduct(ProductDto product)
        {
            var newImageName = string.Empty;

            if (product.Image != null)
            {
                newImageName = _imageManager.UploadImage(product.ImageName, product.Image, PRODUCT_PATH);
            }
            else
            {
                newImageName = product.ImageName;
            }

            

            var EditProduct = GetProduct(product.ProductId);
            if (EditProduct != null)
            {
                EditProduct.Image = product.ImageName;
                EditProduct.Title = product.Title;
                EditProduct.Tags = product.Tags;
                EditProduct.Description = product.Description;
                EditProduct.Content = product.Content;
                EditProduct.Image = newImageName;
            }

            _context.Update(EditProduct);
            _context.SaveChanges();
        }


        #endregion
    }
}
