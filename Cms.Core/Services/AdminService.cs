using Cms.Core.DTOs;
using Cms.Core.DTOs.AdminPanel;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services
{
    public class AdminService : IAdminService
    {
        private CmsContext _context;

        public AdminService(CmsContext context)
        {
            _context = context;
        }

        public UsersDto GetDeletedUsers(int pageId = 1, string filterByEmail = "", string filterByUserName = "")
        {
            IQueryable<User> users = _context.Users.IgnoreQueryFilters().Where(u => u.IsDeleted);

            if (!string.IsNullOrEmpty(filterByEmail))
            {
                users = users.Where(u => u.Email.Contains(filterByEmail));
            }
            if (!string.IsNullOrEmpty(filterByUserName))
            {
                users = users.Where(u => u.UserName.Contains(filterByUserName));
            }


            int take = 10;
            int skip = Math.Abs(pageId - 1) * take;
            var result = new UsersDto();
            //result.Users = users;
            result.CurrentPage = pageId;
            result.TotalPage = users.Count() / take < 1 ? 1 : users.Count() / take;
            result.Users = users.OrderBy(k => k.RegisteredDate).Skip(skip).Take(take).ToList();


            return result;
        }

        public EditUserDto GetUserForEdit(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).Select(user => new EditUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                //Password = user.Password,
                AvatarName = user.Avatar,
                RoleIds = user.UserRoles.Select(r => r.RoleId).ToList(),
            }).Single();
        }

        public UsersDto GetUsers(int pageId = 1, string filterByEmail = "", string filterByUserName = "")
        {
            IQueryable<User> users = _context.Users;

            if (!string.IsNullOrEmpty(filterByEmail))
            {
                users = users.Where(u => u.Email.Contains(filterByEmail));
            }
            if (!string.IsNullOrEmpty(filterByUserName))
            {
                users = users.Where(u => u.UserName.Contains(filterByUserName));
            }

            
            int take = 10;
            int skip = Math.Abs(pageId - 1) * take;
            var result = new UsersDto();
            //result.Users = users;
            result.CurrentPage = pageId;
            result.TotalPage =  users.Count() / take < 1 ? 1 : users.Count() / take;
            result.Users = users.OrderBy( k => k.RegisteredDate).Skip(skip).Take(take).ToList();


            return result;
        }
    }
}
