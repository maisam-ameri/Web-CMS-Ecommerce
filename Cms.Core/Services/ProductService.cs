using Cms.Core.DTOs.AdminPanel.Product;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities.Shop;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        public IEnumerable<SelectListItem> GetCategories(int? parentId = null)
        {
            var categories = _context.Categories.Where(p => p.ParentId == parentId).Select(k => new SelectListItem
            {
                Value = k.Id.ToString(),
                Text = k.Title
            }).ToList();
            return categories;
        }

        public IEnumerable<CategoryDto> GetCategoryDtos() => _context.Categories.Select(c => new CategoryDto
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

        public IEnumerable<SelectListItem> GetProductsForSelectList()
        {
            return _context.Products.Where(p=> p.DiscountId == null).Select(p => new SelectListItem
            {
                Value = p.ProductId.ToString(),
                Text = p.Title
            });
        }


        public Product GetProduct(int? id)
        {
            return _context.Products.Find(id);
        }

        public void CreateProduct(ProductDto productDto)
        {
            var product = new Product
            {
                Title = productDto.Title,
                Description = productDto.Description,
                Content = productDto.Content,
                Tags = productDto.Tags,
                Image = productDto.ImageName,
                IsDeleted = false,
                CategoryId = productDto.CategoryId,
                RegisterDate = DateTime.Now,
                Price = productDto.Price,
            };

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
                CategoryId = p.CategoryId.Value,
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
                EditProduct.Title = product.Title;
                EditProduct.Tags = product.Tags;
                EditProduct.Description = product.Description;
                EditProduct.Content = product.Content;
                EditProduct.Image = newImageName;
                EditProduct.Price = product.Price;
            }

            _context.Update(EditProduct);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = GetProduct(id);
            if (product != null)
            {
                product.IsDeleted = true;
                _context.SaveChanges();
            }

        }

        #endregion




        #region Shop

        private readonly int _takeValue = 8;

        public IEnumerable<Cms.Core.DTOs.Shop.ProductDto> GetLastProducts()
        {
            return _context.Products.Select(p => new DTOs.Shop.ProductDto
            {
                ImageName = p.Image,
                Price = p.Price,
                Title = p.Title
            }).ToList();
        }

        public List<Cms.Core.DTOs.Shop.ProductDto> GetProductsForShop(int pageId = 1, string keyword = "", int minPrice = 0, int maxPrice = int.MaxValue, List<int>? selectedCategories = null)
        {
            IEnumerable<Product> products = _context.Products;

            if (!string.IsNullOrEmpty(keyword))
                products = products
                .Where(p =>
            p.Title.Contains(keyword) ||
            p.Content.Contains(keyword) ||
            p.Description.Contains(keyword) ||
            p.Tags.Contains(keyword)
            ).ToList();

            products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();

            if (selectedCategories != null && selectedCategories.Count > 0)
            {
                var categories = _context.Categories.Where(c =>  selectedCategories.Any(s => s == c.Id)).ToList();

                products = products.Where(p => categories.Any(c => c.Id == p.CategoryId)).ToList();
            }

            var skip = (pageId - 1) * _takeValue;
            products = products.Skip(skip).Take(pageId * _takeValue).ToList();

            var productDtos = products.Select(p => new Cms.Core.DTOs.Shop.ProductDto()
            {
                Id = p.ProductId,
                ImageName = p.Image,
                Price = p.Price,
                Title = p.Title,
            }).ToList();

            return productDtos;
        }

        public int GetTotalProductPageCount()
        {
            return  (int) Math.Ceiling(_context.Products.Count() / (float)_takeValue);
           
        }

        #endregion
    }
}
