using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Entities.Course
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage ="{0} نمیتواند بیستر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "حذف شده؟")]
        public bool IsDeleted { get; set; }

        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }

        
        [ForeignKey(nameof(ParentId))]
        public List<ProductCategory>? ProductCategories { get; set; }


    }
}
