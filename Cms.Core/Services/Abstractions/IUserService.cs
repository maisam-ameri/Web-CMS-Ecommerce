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

        bool ActiveAccount(string activeCode);
    }
}
