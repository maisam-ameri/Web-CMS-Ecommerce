using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Entities
{
    public class User
    {
        public User()
        {
            
        }

        [Key]
        public int Id { get; set; }

        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage ="{0} نمیتواند بیستر از {1} باشد")]
        public string UserName { get; set; }

        [Display(Name ="نام")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage ="{0} نمیتواند بیستر از {1} باشد")]
        public string FirstName { get; set; }

        [Display(Name ="نام خانوادگی")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage ="{0} نمیتواند بیستر از {1} باشد")]
        public string LastName { get; set; }

        [Display(Name ="ایمیل")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Email { get; set; }

        [Display(Name ="رمز عبور")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name ="آدرس")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage ="{0} نمیتواند بیستر از {1} باشد")]
        public string Address { get; set; }

        [Display(Name ="شماره تماس")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Phone { get; set; }

        public string ActivateCode { get; set; }

        public bool IsActive { get; set; }

        [Display(Name ="نمایه")]
        public string Avatar { get; set; }

        [Display(Name ="تاریخ تولد")]
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public DateTime BirthDate { get; set; }

        public DateTime RegisteredDate { get; set; }



        public virtual List<UserRole> UserRoles { get; set; }
    }
}
