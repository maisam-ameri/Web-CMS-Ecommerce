using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Entities.Shop
{
    public class Discount
    {
        public Discount()
        {
            
        }

        [Key]
        public int DiscountId { get; set; }
        
        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage ="{0} نمیتواند بیستر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name ="درصد تخفیف")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [Range(1, 100, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public int Amount{ get; set; }
        
        [Display(Name ="تاریخ شروع")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public DateTime StartDate { get; set; }

        [Display(Name ="تاریخ پایان")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public DateTime EndDate { get; set; }
        
        [Required]
        public bool IsActive { get; set; }


        public List<Product> Products { get; set; }
    }
}
