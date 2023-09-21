using Cms.DataLayer;
using Cms.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.DTOs.AdminPanel
{
    public class UsersDto
    {

        public IEnumerable<User> Users{ get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}
