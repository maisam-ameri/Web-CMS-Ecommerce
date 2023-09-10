using Cms.Core.Generators;
using Cms.Core.Security;
using Cms.DataLayer;
using Cms.DataLayer.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services
{
    public class UserService : IUserService
    {
        private CmsContext _context;

        public UserService(CmsContext context)
        {
            _context = context;
        }
        public bool IsExistUserName(string username)
        {
            return _context.Users.Any(_u => _u.UserName == username);
        }
        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public int CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public User LoginUser(LoginDto login)
        {
            var hashPassword = PasswordHash.EncodePasswordMd5(login.Password);

            var user = _context.Users.SingleOrDefault(u => u.UserName == login.UserName && u.Password == hashPassword);

            return user;
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActivateCode == activeCode);

            if(user == null || user.IsActive)
            {
                return false;
            }

            user.IsActive = true;
            user.ActivateCode = NameGenerator.GenerateName();
            _context.SaveChanges();

            return true;

        }
    }
}
