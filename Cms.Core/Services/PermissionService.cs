using Cms.Core.Services.Abstractions;
using Cms.DataLayer;
using Cms.DataLayer.Context;
using Cms.DataLayer.Entities;
using Cms.DataLayer.Entities.Permission;
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

        #region Roles

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

        public IEnumerable<UserRole> GetUserRoles(string username)
        {
            var userRoles = new List<UserRole>();
            var userId = _context.Users.SingleOrDefault(u => u.UserName == username).Id;
            return _context.UserRoles.Where(r => r.UserId == userId);
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

        public void UpdateRole(Role role,List<int> permissions = null)
        {
            AssignPermissionToRole(role.RoleId, permissions);
            _context.Update(role);
            _context.SaveChanges();
        }

        public void DeleteRole(int roleId)
        {
            var role = GetRole(roleId);
            role.IsDelete = true;
            UpdateRole(role);
        }


        #endregion

        #region Permissions

        public List<Permission> GetPermissions()
        {
            return _context.Permissions.ToList();
        }

        public void AssignPermissionToRole(int roleId, List<int> selectedPermission = null)
        {
            UnassignedPermissionRole(roleId);
            var permissions = new List<RolePermission>();

            if (selectedPermission != null)
            {

                selectedPermission.ForEach(p =>
                {
                    permissions.Add(new RolePermission
                    {
                        PermissionId = p,
                        RoleId = roleId
                    });
                });

            }
            _context.RolePermissions.AddRange(permissions);
            _context.SaveChanges();
        }

        public List<int> GetPermissionsRole(int roleId)
        {
            return _context.RolePermissions.Where(p => p.RoleId == roleId).Select(k => k.PermissionId).ToList();
        }

        public void UnassignedPermissionRole(int roleId)
        {
            _context.RolePermissions.RemoveRange(_context.RolePermissions.Where(u => u.RoleId == roleId));
        }

        public bool CheckPermission(int permissionId, string username)
        {
            var userRoles = GetUserRoles(username).ToList();
            if (!userRoles.Any()) return false;
            var permissions = _context.RolePermissions.Where(p => p.PermissionId == permissionId);

            var isAny = userRoles.Any(u => permissions.Any(p => p.RoleId ==  u.RoleId));
            return isAny;
        }
        #endregion
    }
}
