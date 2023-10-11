using Cms.Core.DTOs.AdminPanel.Product;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities.Product;
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

        public int CreateCategory(Category category)
        {
            var newCat =_context.Categories.Add(category);

            _context.SaveChanges();
            return newCat.Entity.Id;

        }

        public IEnumerable<Category> GetCategories() =>  _context.Categories.ToList();

        public Category GetCategory(int id)
        {
            return _context.Categories.SingleOrDefault(c => c.Id == id);
        }

        public List<CategoryDto> GetParentCategoriesForAdminPanel()
        {
            return _context.Categories.Where( c => c.ParentId == null).Select(c => new CategoryDto
            {
                Id  = c.Id,
                Title = c.Title,
            }).ToList();
        }

        public void UpdateCategory(Category category)
        {
            _context.Update(category);
            _context.SaveChanges();
        }
    }
}
