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
            throw new NotImplementedException();
        }
        public int CreateContentCategory(Category newCategory)
        {
            var entity = _context.ContentCategories.Add(newCategory);
            _context.SaveChanges();
            return entity.Entity.CategoryId;
        }


        #endregion
    }
}
