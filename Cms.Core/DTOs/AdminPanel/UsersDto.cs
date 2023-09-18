using Cms.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.DTOs
{
    public class UsersDto
    {

        public IEnumerable<User> Users{ get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}
