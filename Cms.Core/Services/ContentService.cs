using Cms.Core.DTOs.AdminPanel.Content;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities.Content;
using Hangfire;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private ImageManager _imageManager { get; set; }
        private const string CONTENT_PATH = "images/content/";


        public ContentService(CmsContext context, ImageManager imageManager)
        {
            _context = context;
            _imageManager = imageManager;
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

        #region Content
        public List<BaseContent> GetBaseContents()
        {
            return _context.BaseContents.ToList();
        }

        public string CreateBaseContent(BaseContentDto content)
        {
            var imageName = _imageManager.UploadImage("", content.ImageFile, CONTENT_PATH);
            var newContent = new BaseContent
            {
                ImageName = imageName,
                IsDeleted = false,
                MainText = content.MainText,
                PublishDate = content.PublishDate,
                ShortDescription = content.ShortDescription,
                ShowInMainMenu = content.ShowInMainMenu,
                Tags = content.Tags,
                Title = content.Title,
                ViewCount = 0,
                CategoryId = content.CategoryId,
            };



            var jobId = BackgroundJob.Schedule(() => CreateContentJob(newContent), newContent.PublishDate);

            return jobId;

        }

        public void CreateContentJob(BaseContent content)
        {
                _context.Add(content);
                _context.SaveChanges();
        }

        public void DeleteBaseContent(int id)
        {
            throw new NotImplementedException();
        }


        public BaseContent GetBaseContent(int categoryId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBaseContent(BaseContent category)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
