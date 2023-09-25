using Cms.DataLayer.Entities;
using Cms.DataLayer.Entities.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Context
{
    public class CmsContext:DbContext
    {

        public CmsContext(DbContextOptions<CmsContext> options ):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDelete);
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Permission> Permissions{ get; set; }
        public DbSet<RolePermission> RolePermissions{ get; set; }

    }
}
