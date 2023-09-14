using Cms.Core.DTOs;
using Cms.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services
{
    public interface IUserService
    {
        bool IsExistUserName(string username);
        bool IsExistEmail(string email);
        int CreateUser(User user);
        User LoginUser(LoginDto login);
        User GetUserByEmail(string email);
        User GetUserByUserName(string username);
        User GetUserByActiveCode(string activeCode);
        void UpdateUser(User user);
        bool ActiveAccount(string activeCode);

        #region User Panel
        InformationUserDto GetUserInformation(string username);
        SideBarUserPanelDto GetSidebarUserPanel(string username);
        #endregion
    }
}
