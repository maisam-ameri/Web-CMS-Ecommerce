using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.DTOs.Shop
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string ImageName { get; set; }
        
        [Display(Name = "قیمت")]
        public double? Price { get; set; }
    }
}
