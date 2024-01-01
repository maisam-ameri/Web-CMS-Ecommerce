using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Entities.Content
{
    public class ContentComment
    {
        public ContentComment()
        {
            
        }

        [Key]
        public int CommentId { get; set; }

        public int ContentId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150)]
        public string? Name { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200)]
        public string? Email { get; set; }

        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500)]
        public string? Comment { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }
        
        
        public BaseContent? Content { get; set; }
    }
}
