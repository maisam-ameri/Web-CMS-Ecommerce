using Cms.Core.Services.Abstractions;
using Cms.DataLayer;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private CmsContext _context;

        public PermissionService(CmsContext context)
        {
            _context = context;
        }

        public UserRole AssignUserRoles(int userId, List<int> roles)
        {
            UnassignUserRoles(userId);
            var newUserRole = new List<UserRole>();
            foreach (var role in roles)
            {
                newUserRole.Add(new UserRole { RoleId = role, UserId = userId });
            }
            _context.UserRoles.AddRange(newUserRole);
            _context.SaveChanges();

            return null;
        }

        public void UnassignUserRoles(int userId)
        {
            _context.UserRoles.RemoveRange(_context.UserRoles.Where(u => u.UserId == userId));
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles;
        }

        public IEnumerable<UserRole> GetUserRoles(string username, List<int> roles)
        {
            var userRoles = new List<UserRole>();
            var userId = _context.Users.SingleOrDefault(u => u.UserName == username).Id;

            foreach (var role in GetRoles())
            {
                roles.ForEach((r) =>
                {
                    if (r == role.RoleId)
                        userRoles.Add(new UserRole
                        {
                            RoleId = role.RoleId,
                            UserId = userId
                        });
                });
            }
            return userRoles;
        }

        public void DeleteUserRoles(int userId)
        {
            UnassignUserRoles(userId);
        }

        public Role GetRole(int roleId)
        {
            return _context.Roles.SingleOrDefault(r => r.RoleId == roleId);
        }

        public int CreateRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public void UpdateRole(Role role)
        {
            _context.Update(role);
            _context.SaveChanges();
        }

        public void DeleteRole(int roleId)
        {
            var role = GetRole(roleId);
            role.IsDelete = true;
            UpdateRole(role);
        }

    }
}
