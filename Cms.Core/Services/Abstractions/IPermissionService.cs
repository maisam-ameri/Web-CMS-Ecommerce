using Cms.DataLayer;
using Cms.DataLayer.Entities;
using Cms.DataLayer.Entities.Permission;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services.Abstractions
{
    public interface IPermissionService
    {
        #region Roles

        IEnumerable<Role> GetRoles();
        List<SelectListItem> GetRolesForSelectList();
        IEnumerable<UserRole> GetUserRoles(string username, List<int> roles);
        UserRole AssignUserRoles(int userId, List<int> roleId);
        void UnassignUserRoles(int userId);

        Role GetRole(int roleId);
        int CreateRole(Role role);
        void UpdateRole(Role role, List<int> permissions);
        void DeleteRole(int roleId);

        #endregion

        #region Permissions

        List<Permission> GetPermissions();
        void AssignPermissionToRole(int roleId, List<int> selectedPermission);
        void UnassignedPermissionRole(int roleId);
        List<int> GetPermissionsRole(int roleId);

        bool CheckPermission(int permissionId, string username);

        #endregion

    }
}
