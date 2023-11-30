using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Entities.Shop
{
    public class DiscountCode
    {
        public DiscountCode()
        {

        }

        [Key]
        public int DiscountCodeId { get; set; }
        [Required]
        public int UserId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Code { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1,100,ErrorMessage = "{0} نمیتواند بیشتر از {1} باشد")]
        public int Amount { get; set; }

        [Display(Name = "تاریخ شروع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
