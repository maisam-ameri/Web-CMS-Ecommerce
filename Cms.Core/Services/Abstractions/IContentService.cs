using Cms.Core.DTOs.AdminPanel.Content;
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
        #region Category
        int CreateContentCategory(Category newCategory);
        void DeleteCategory(int id);
        List<Category> GetCategories();
        Category GetCategory(int categoryId);
        void UpdateContentCategory(Category category);

        #endregion

        #region BaseContent
        string CreateBaseContent(BaseContentDto newContent);
        void DeleteBaseContent(int id);
        List<BaseContent> GetBaseContents();
        BaseContent GetBaseContent(int id);
        void UpdateBaseContent(BaseContentDto content);
        List<ContentCategoryDto> GetContentCategoryDtos();
        BaseContentDto GetContentForEditInAdmin(int id);

        #endregion
    }
}
