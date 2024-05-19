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

        //     protected override void OnModelCreating(ModelBuilder modelBuilder)
        //     {
        //         base.OnModelCreating(modelBuilder);

        //var addresses = new List<Address>()
        //{
        //	new Address()
        //	{
        //		Id: Guid.Parse("6f1c1751-b42c-4c22-93b0-9f83e4e7b92d"),
        //		Name: "Khartoum"
        //	}
        //};

        //     }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            new BaseEntityTypeConfiguration().Configure(builder.Entity<BaseEntity>());
            new CategoryEntityTypeConfiguration().Configure(builder.Entity<Category>());
            new ProductEntityTypeConfiguration().Configure(builder.Entity<Product>());

            //builder.ApplyConfigurationsFromAssembly(typeof(AddressEntityTypeConfiguration).Assembly);

            // exculasice - to ignore any change on data base 
            //builder.Entity<Address>()
            //    .ToTable("Addresses", a => a.ExcludeFromMigrations());
            

            var readRoleId = "B136F701-C04F-41B0-B327-2AF20B283548";
            var writeRoleId = "813620DA-EB26-4F68-AFFA-A381D55171B0";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readRoleId,
                    ConcurrencyStamp = readRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },

                new IdentityRole
                {
                    Id = writeRoleId,
                    ConcurrencyStamp = writeRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

