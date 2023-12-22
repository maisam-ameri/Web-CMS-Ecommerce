using Cms.DataLayer.Entities.Shop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.DTOs.UserPanel
{
    public class UserOrderDetailDto
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "عنوان")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "تعداد")]
        public int Count { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime RegisterDate { get; set; }

        public int DiscountAmount { get; set; }
    }
}
