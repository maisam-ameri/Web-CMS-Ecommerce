using Cms.DataLayer;
using Cms.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Services.Abstractions
{
    public interface IPermissionService
    {
        IEnumerable<Role> GetRoles();
        IEnumerable<UserRole> GetUserRoles(string username, List<int> roles);
        UserRole AssignUserRoles(int userId, List<int> roleId);
        void UnassignUserRoles(int userId);

        #region Role CRUD

        Role GetRole(int roleId);
        void CreateRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(int roleId);

        #endregion

    }
}
