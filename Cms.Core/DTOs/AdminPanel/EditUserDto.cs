using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.DTOs.AdminPanel
{
    public class EditUserDto
    {
        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیستر از {1} باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string? Email { get; set; }

        [Display(Name = "رمز عبور")]
        public string? Password { get; set; }

        
        public string? AvatarName { get; set; }
        public IFormFile? Avatar { get; set; }

        public List<int>? RoleIds { get; set; }
    }
}
