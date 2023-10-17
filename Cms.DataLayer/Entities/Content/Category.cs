using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Entities.Content
{
    public class Category
    {

        public Category()
        {

        }

        [Key]
        public int CategoryId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        public string Title { get; set; }

        public bool IsDeleted { get; set; }


        public List<BaseContent>? Contents { get; set; }
    }
}
