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
    public class PermissionService:IPermissionService
    {
        private CmsContext _context;

        public PermissionService(CmsContext context)
        {
            _context = context;
        }
        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles;
        }
    }
}
