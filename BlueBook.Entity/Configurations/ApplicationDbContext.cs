using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using BlueBook.DataAccess.Entities;
using BlueBook.DataAccess.Mappings;
using System;
using System.Data.Common;

namespace BlueBook.DataAccess.Configurations
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("BlueBook", throwIfV1Schema: false)
        {
        }

        public ApplicationDbContext(DbConnection context)
            :base(context,true)
        {

        }

        DbSet<Brand> Brands { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<MarketHierarchy> MarketHierarchies { get; set; }
        DbSet<FieldForce> FieldForces { get; set; }
        DbSet<Distributor> Distributors { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new FieldForceMapping());
            modelBuilder.Configurations.Add(new BrandMapping());
            modelBuilder.Configurations.Add(new ProductMapping());
            modelBuilder.Configurations.Add(new MarketHierarchyMapping());
            modelBuilder.Configurations.Add(new DistributorMapping());
            modelBuilder.Configurations.Add(new FieldForceAdressMapping());
        }

    }
}