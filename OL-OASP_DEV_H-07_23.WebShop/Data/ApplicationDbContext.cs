using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.Common;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.CompanyModels;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.ProductModels;
using OL_OASP_DEV_H_07_23.WebShop.Models.Dbo.UserModel;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Interfaces;

namespace OL_OASP_DEV_H_07_23.WebShop.Data
{
    /// <summary>
    /// Application db context
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


                        modelBuilder.Entity<QuantityType>().HasData(
                new QuantityType
                {
                    Id = 1,
                    Name = "Dan",
                    Created = new DateTime(2024, 5, 22),
                    Valid = true
                },
                new QuantityType
                {
                    Id = 2,
                    Name = "Mjesec",
                    Created = new DateTime(2024, 5, 22),
                    Valid = true
                },
                new QuantityType
                {
                    Id = 3,
                    Name = "Godina",
                    Created = new DateTime(2024, 5, 22),
                    Valid = true
                },
                new QuantityType
                {
                    Id = 4,
                    Name = "Kg",
                    Created = new DateTime(2024, 5, 22),
                    Valid = true
                },
                new QuantityType
                {
                    Id = 5,
                    Name = "Litra",
                    Created = new DateTime(2024, 5, 22),
                    Valid = true
                },
                new QuantityType
                {
                    Id = 6,
                    Name = "Dg",
                    Created = new DateTime(2024, 5, 22),
                    Valid = true
                },
                new QuantityType
                {
                    Id = 7,
                    Name = "Komad",
                    Created = new DateTime(2024, 5, 22),
                    Valid = true
                }
            );


            modelBuilder.Entity<Address>().HasData(
                    new Address
                    {
                        Id = 1,
                        City = "Zagreb",
                        Created = new DateTime(2024, 5, 22),
                        Country = "Hrvatska",
                        Street = "Maksimirska",
                        Number = "100",
                        Valid = true
                    }
            );



            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Created = new DateTime(2024, 5, 22),
                    AddressId = 1,
                    FullName = "Tvrtka d.o.o.",
                    ShortName = "Tvrtka",
                    VAT = "71834573974",
                    Valid = true
                }
            );

            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges()
        {

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IBaseTableAtributes && (
                e.State == EntityState.Added || e.State == EntityState.Modified));


            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Modified:
                        ((IBaseTableAtributes)entityEntry.Entity).Updated = DateTime.Now;
                        break;
                    case EntityState.Added:
                        ((IBaseTableAtributes)entityEntry.Entity).Valid = true;
                        ((IBaseTableAtributes)entityEntry.Entity).Created = DateTime.Now;
                        break;
                    default:
                        break;
                }

            }
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {

            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is IBaseTableAtributes && (
              e.State == EntityState.Added
              || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Deleted:
                        entityEntry.State = EntityState.Modified;
                        ((IBaseTableAtributes)entityEntry.Entity).Valid = false;
                        break;
                    case EntityState.Modified:
                        ((IBaseTableAtributes)entityEntry.Entity).Updated = DateTime.Now;
                        break;
                    case EntityState.Added:
                        ((IBaseTableAtributes)entityEntry.Entity).Valid = true;
                        ((IBaseTableAtributes)entityEntry.Entity).Created = DateTime.Now;
                        break;
                    default:
                        break;
                }

            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        #region ProductModels
        public DbSet<ProductCategory> ProductCategorys { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<QuantityType> QuantityTypes { get; set; }
        #endregion

        #region CompanyModels
        public DbSet<Company> Companys { get; set; }
        #endregion
        #region Common
        public DbSet<Address> Addresss { get; set; }
        #endregion
    }
}
