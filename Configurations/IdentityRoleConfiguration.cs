using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityRole> builder)
        {
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

            builder.HasData(roles);
        }
    }
}