using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Entities.Shop
{
    public class DiscountCodeUser
    {
        [Key]
        public int DisCodeUserId { get; set; }
        
        [Required]
        public int DiscountCodeId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public string Code { get; set; }

    }
}
