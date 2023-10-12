using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Entities.Product
{
    public class Product
    {
        public Product()
        {
            
        }
        [Key]
        public int ProductId { get; set; }
        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage ="{0} نمیتواند بیستر از {1} باشد")]
        public string Title { get; set; }
        [Display(Name ="توضیحات")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage ="{0} نمیتواند بیستر از {1} باشد")]
        public string Description{ get; set; }

        [Display(Name = "محتوا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(1000, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "تگ ها")]
        [MaxLength(500, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string? Tags { get; set; }
        [Display(Name = "تصویر")]
        public string? Image { get; set; }



        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
    }
}
