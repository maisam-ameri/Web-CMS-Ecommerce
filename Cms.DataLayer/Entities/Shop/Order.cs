using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Entities.Shop
{
    public class Order
    {
        public Order()
        {
            
        }

        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        public int OrderSum { get; set; }
        public bool IsFinally { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
