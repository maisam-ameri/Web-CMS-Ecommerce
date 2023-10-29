using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.DTOs.AdminPanel.Content
{
    public class BaseContentDto
    {

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string ShortDescription { get; set; }

        [Display(Name = "متن اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(2000, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string MainText { get; set; }

        [Display(Name = "تگ ها")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string? Tags { get; set; }
        
        [Display(Name = "تصویر")]
        public IFormFile? ImageFile { get; set; }
        
        public string? ImageName { get; set; }

        
        [Display(Name = "نمایش در منوی اصلی")]
        public bool ShowInMainMenu { get; set; }

        [Display(Name = "تاریخ انتشار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime PublishDate { get; set; }
    }
}
