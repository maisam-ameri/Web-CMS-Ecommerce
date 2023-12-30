using Cms.Core.DTOs.AdminPanel.Content;
using Cms.Core.FileManager;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities.Content;
using Cms.Core.Generators;
//using Cms.DataLayer.Entities.Shop;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Cms.Core.DTOs.Content;

namespace Cms.Core.Services
{
    public class ContentService : IContentService
    {
        private CmsContext _context;
        private ImageManager _imageManager { get; set; }
        private const string CONTENT_PATH = "images/content/";
        private const string CONTENT_THUBMNAIL_PATH = "images/content/thumbnail/";


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
            var imageName = string.Empty;
            if (content.ImageFile.IsImage())
            {

                imageName = _imageManager.UploadImage("", content.ImageFile, CONTENT_PATH);
                
                _imageManager.GenerateThumbnail(imageName, CONTENT_PATH, CONTENT_THUBMNAIL_PATH, 150);
            }

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
                IsPublished = false
            };


            var addedContent = _context.BaseContents.Add(newContent);
            _context.SaveChanges();
            var contentId = addedContent.Entity.ContentId;

            var jobId = BackgroundJob.Schedule(() => PublishContentJob(contentId), newContent.PublishDate);

            var contentJob = GetBaseContent(contentId);
            contentJob.JobId = int.Parse(jobId);
            _context.Update(contentJob);
            _context.SaveChanges();


            return jobId;

        }

        public bool PublishContentJob(int contentId)
        {
            var content = GetBaseContent(contentId);
            if (content != null)
            {
                content.IsPublished = true;
                _context.Update(content);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void DeleteBaseContent(int id)
        {
            var content = GetBaseContent(id);
            content.IsDeleted = true;
            BackgroundJob.Delete(content.JobId.ToString());
            _context.SaveChanges();
        }


        public BaseContent GetBaseContent(int id)
        {
            return _context.BaseContents.Find(id);
        }

        public List<ContentCategoryDto> GetContentCategoryDtos()
        {
            return _context.ContentCategories.Select(c => new ContentCategoryDto
            {
                Id = c.CategoryId,
                Title = c.Title,
            }).ToList();
        }

        public BaseContentDto GetContentForEditInAdmin(int id)
        {
            return _context.BaseContents.Where(c => c.ContentId == id)
                .Select(c => new BaseContentDto
                {
                    ContentId = c.ContentId,
                    Title = c.Title,
                    CategoryId = c.CategoryId.Value,
                    ImageName = c.ImageName,
                    MainText = c.MainText,
                    PublishDate = c.PublishDate,
                    ShortDescription = c.ShortDescription,
                    ShowInMainMenu = c.ShowInMainMenu,
                    Tags = c.Tags,

                }
                ).Single();
        }

        public void UpdateBaseContent(BaseContentDto contentDto)
        {
            var newImageName = string.Empty;

            if (contentDto.ImageFile != null)
            {
                newImageName = _imageManager.UploadImage(contentDto.ImageName, contentDto.ImageFile, CONTENT_PATH);
            }
            else
            {
                newImageName = contentDto.ImageName;
            }

            var content = _context.BaseContents.Find(contentDto.ContentId);

            if (content != null)
            {
                content.ShortDescription = contentDto.ShortDescription;
                content.Title = contentDto.Title;
                content.ModifiedDate = DateTime.Now;
                content.CategoryId = contentDto.CategoryId;
                content.ImageName = newImageName;
                content.MainText = contentDto.MainText;
                content.PublishDate = contentDto.PublishDate;
                content.Tags = contentDto.Tags;

            }

            _context.Update(content);
            _context.SaveChanges();
        }

        public List<ContentDto>? GetLatesContents()
        {
            return _context.BaseContents.OrderByDescending(c => c.PublishDate).Take(10).Select(c => new ContentDto
            {
                Title = c.Title,
                ShortDescription = c.ShortDescription,
                ImageName = c.ImageName,
                PublishDate = c.PublishDate,

            }).ToList();
        }

        public List<ContentDto>? GetMostViewsContents()
        {
            return _context.BaseContents.OrderByDescending(c => c.ViewCount).Take(10).Select(c => new ContentDto
            {
                Title = c.Title,
                ShortDescription = c.ShortDescription,
                ImageName = c.ImageName,
                PublishDate = c.PublishDate,

            }).ToList();
        }

        public List<ContentDto>? GetContentsForSlider()
        {
            return _context.BaseContents.Where(c => c.ShowInMainMenu).OrderByDescending(c => c.ViewCount).Select(c => new ContentDto
            {
                Title = c.Title,
                ShortDescription = c.ShortDescription,
                ImageName = c.ImageName,
                PublishDate = c.PublishDate,

            }).ToList();
        }

        #endregion
    }
}
