using System;
using EcommerceAPI.Configurations;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Data
{
	public class DataContext: IdentityDbContext
    {
		public DataContext(DbContextOptions<DataContext> options): base(options)
		{
		}


        public DbSet<Brand> Brands { get; set; }
		public DbSet<Unit> Units { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            new BaseEntityTypeConfiguration().Configure(builder.Entity<BaseEntity>());
            new CategoryEntityTypeConfiguration().Configure(builder.Entity<Category>());
            new ProductEntityTypeConfiguration().Configure(builder.Entity<Product>());
            new IdentityRoleConfiguration().Configure(builder.Entity<IdentityRole>());

            //builder.ApplyConfigurationsFromAssembly(typeof(AddressEntityTypeConfiguration).Assembly);

            // exculasice - to ignore any change on data base 
            //builder.Entity<Address>()
            //    .ToTable("Addresses", a => a.ExcludeFromMigrations());
            

            
        }
    }
}

