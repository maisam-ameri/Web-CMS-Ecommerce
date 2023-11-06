using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Entities.Content
{
    public class BaseContent
    {
        public BaseContent()
        {
            
        }


        [Key]
        public int ContentId { get; set; }

        public int JobId { get; set; }

        [Display(Name = "عنوان")]
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
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }

        [Display(Name = "نمایش در منوی اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool ShowInMainMenu { get; set; }

        [Display(Name = "تاریخ انتشار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime PublishDate { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsPublished { get; set; }
        
        public bool IsDeleted { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int ViewCount { get; set; }


        [Display(Name = "دسته بندی")]
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
