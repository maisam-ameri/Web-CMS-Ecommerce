using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services
{
    public class ContentService : IContentService
    {
        private CmsContext _context;

        public ContentService(CmsContext context)
        {
            _context = context;
        }


        #region Category

        public List<Category> GetCategories()
        {
            return _context.ContentCategories.ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return _context.ContentCategories.Find(categoryId);

        }
        public int CreateContentCategory(Category newCategory)
        {
            var entity = _context.ContentCategories.Add(newCategory);
            _context.SaveChanges();
            return entity.Entity.CategoryId;
        }

        public void UpdateContentCategory(Category category)
        {
            var Cat = GetCategory(category.CategoryId);
            Cat.Title = category.Title;
            _context.Update(Cat);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            _context.ContentCategories.Find(id).IsDeleted = true;
            _context.SaveChanges();
        }


        #endregion
    }
}
