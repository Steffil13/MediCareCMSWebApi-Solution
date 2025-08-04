using MediCareCMSWebApi.Models;
using MediCareCMSWebApi.Repository;
using MediCareCMSWebApi.Service;
using Microsoft.EntityFrameworkCore;

namespace MediCareCMSWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //cors policy

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

            builder.Services.AddDbContext<MediCareDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<ILoginService, LoginService>();

            //swagger 
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Enable CORS
            app.UseCors("AllowAllOrigin");

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
