using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Repository;
using MediCareCMSWebApi.Service;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace MediCareCMSWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                // This fixes the circular reference serialization issue
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            // CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            // Database context
            builder.Services.AddDbContext<MediCareDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Repository & Service for Login
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<ILoginService, LoginService>();

            // Repository & Service for Pharmacist
            builder.Services.AddScoped<IPharmacistRepository, PharmacistRepository>();
            builder.Services.AddScoped<IPharmacistService, PharmacistService>();

            // Repository & Service for Lab Technician
            builder.Services.AddScoped<ILabTechnicianRepository, LabTechnicianRepository>();
            builder.Services.AddScoped<ILabTechnicianService, LabTechnicianService>();

            // Repository & Service for User
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Swagger
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Swagger UI for development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable CORS
            app.UseCors("AllowAllOrigin");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
