using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Entities
{
    public class Role
    {
        public Role()
        {
            
        }

        [Key]
        public int RoleId { get; set; }
        [Display(Name ="عنوان نقش")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage ="{0} نمیتواند بیشتر از {1} باشد")]
        public string RoleTitle { get; set; }

        public bool IsDelete { get; set; }


        public virtual List<UserRole>? UserRoles{ get; set; }
    }
}
