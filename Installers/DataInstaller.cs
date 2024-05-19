using System;
using EcommerceAPI.Data;
using EcommerceAPI.Data.Dto;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Models;
using EcommerceAPI.Repository;
using EcommerceAPI.Services;
using EcommerceAPI.Validations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EcommerceAPI.Installers
{
    public class DataInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection service, IConfiguration configuration, IWebHostEnvironment env)
        {
            // server connection  DB
            service.AddDbContext<DataContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            #region inject the controller
            service.AddScoped<IBrandRepository, BrandRepository>();
            service.AddScoped<ITokenRepository, TokenRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            service.AddTransient<IFileRepository, FileService>();
            #endregion


            #region Validation
            service.AddTransient<IValidator<AddressRequestDto>, AddressValidation>();
            service.AddTransient<IValidator<ProductRequestDto>, ProductValidation>();
            service.AddTransient<IValidator<UnitRequestDto>, UnitValidation>();
            #endregion

        }


    }
}

