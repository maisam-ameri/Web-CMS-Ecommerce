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

    }
}
