using System;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceAPI.Configurations
{
    public class BaseEntityTypeConfiguration : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder
                .Property(b => b.UpdatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}

