using System.ComponentModel.DataAnnotations;

namespace Cms.DataLayer.Entities.Shop
{
    public class OrderDetail
    {
        public OrderDetail()
        {
            
        }

        [Key]
        public int OrderDetailId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Display(Name ="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage ="{0} نمیتواند بیستر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "تعداد")]
        public int Count { get; set; }

        [Display(Name ="قیمت")]
        public int Price { get; set; }

        [Required]
        [Display(Name = "تاریخ")]
        public DateTime RegisterDate { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}