using Cms.DataLayer.Entities.Shop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.DTOs.Shop
{
    public class ShowProductDto
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public string? ImageName { get; set; }
        public string? Content { get; set; }
        public string? Tags { get; set; }
        public DateTime RegisterDate { get; set; }
        public Discount? Discount{ get; set; }
        //public List<ProductComment> Comments { get; set; }
    }
}
