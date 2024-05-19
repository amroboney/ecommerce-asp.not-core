using System.Text;
using EcommerceAPI.Data;
using EcommerceAPI.Installers;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Middleware;
using EcommerceAPI.Models;
using EcommerceAPI.Repository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
//.AddFluentValidation(
//    v => v.RegisterValidatorsFromAssemblyContaining<Address>());


builder.Services.InstallServicesInAssembly(builder.Configuration, builder.Environment);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region Logger Confirmation
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/Ecommerce_logs", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#endregion

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceprtionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = builder.Configuration["RequestFilePath"]
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

