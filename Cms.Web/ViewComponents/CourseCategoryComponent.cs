using Cms.Core.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Cms.Web.ViewComponents
{
    public class CourseCategoryComponent : ViewComponent
    {
        private ICourseService _couserCategoryService;

        public CourseCategoryComponent(ICourseService courseService)
        {
            _couserCategoryService = courseService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var courses = _couserCategoryService.GetCategories();
            return await Task.FromResult((IViewComponentResult) View("CourseCategory", courses));
        }

    }
}
