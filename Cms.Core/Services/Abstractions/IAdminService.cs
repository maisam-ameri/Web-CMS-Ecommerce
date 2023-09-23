using Cms.Core.DTOs;
using Cms.Core.DTOs.AdminPanel;
using Cms.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services.Abstractions
{
    public interface IAdminService
    {
        UsersDto GetUsers(int pageId=1, string filterByEmail="", string filterByUserName="");

        UsersDto GetDeletedUsers(int pageId = 1, string filterByEmail = "", string filterByUserName = "");

        EditUserDto GetUserForEdit(int userId);
    }
}
