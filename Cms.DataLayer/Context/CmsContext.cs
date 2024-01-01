using Cms.DataLayer.Entities;
using Cms.DataLayer.Entities.Content;
using Cms.DataLayer.Entities.Permission;
using Cms.DataLayer.Entities.Shop;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.DataLayer.Context
{
    public class CmsContext : DbContext
    {

        public CmsContext(DbContextOptions<CmsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
        .SelectMany(t => t.GetForeignKeys())
        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;



            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(r => !r.IsDelete);
            modelBuilder.Entity<Cms.DataLayer.Entities.Shop.Category>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(c => !c.IsDeleted);

            modelBuilder.Entity<Cms.DataLayer.Entities.Content.Category>().HasQueryFilter(c => !c.IsDeleted);

            modelBuilder.Entity<BaseContent>().HasQueryFilter(c => !c.IsDeleted);

 
        }

        #region User
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion


        #region Permissions
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        #endregion


        #region Shop
        public DbSet<Cms.DataLayer.Entities.Shop.Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<DiscountCodeUser> DiscountCodeUsers { get; set; }

        #endregion


        #region Content
        public DbSet<Cms.DataLayer.Entities.Content.Category> ContentCategories { get; set; }
        public DbSet<BaseContent> BaseContents { get; set; }

        public DbSet<ContentComment> Comments { get; set; }

        #endregion


    }
}
