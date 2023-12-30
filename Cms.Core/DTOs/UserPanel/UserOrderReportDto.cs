using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.DTOs.UserPanel
{
    public class UserOrderReportDto
    {
        public List<UserOrderDetailDto>? OrderDetails { get; set; }
        public int? FinalPrice { get; set; }
    }
}
