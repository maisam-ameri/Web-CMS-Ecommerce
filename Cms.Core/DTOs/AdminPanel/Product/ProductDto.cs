using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.DTOs.AdminPanel.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string Title { get; set; }
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string Description { get; set; }

        [Display(Name = "محتوا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(1000, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string Content { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }

        [Display(Name = "تگ ها")]
        public string? Tags { get; set; }

        [Required]
        [Display(Name ="قیمت")]
        public int Price { get; set; }
        
        [Display(Name = "تاریخ ثبت")]
        public DateTime RegisterDate { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
