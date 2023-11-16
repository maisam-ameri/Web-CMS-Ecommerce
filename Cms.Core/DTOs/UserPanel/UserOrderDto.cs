using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.DTOs.UserPanel
{
    public class UserOrderDto
    {
        [Required]
        public int OrderId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "تعداد سفارشات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int DetailOrderCount { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsFinally { get; set; }
    }
}
