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

        public int Count { get; set; }
        public int Price { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}