
using E_Commerce.Core.Interface;
using E_Commerce.Core.Models;
using E_Commerce.Core.Services;
using E_Commerce.EF.Repository;
using E_Commerce.EF;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //custom service
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("db"));
            });

            // Regester Services
            // you should declaration interface and concrete class
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>)); // Register Generic Repository
            builder.Services.AddScoped<productService>(); // Register product service
            builder.Services.AddScoped<OrderService>(); // Register order service

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "https://ecommerce-payment.vercel.app", "https://interactive-card-details-ecru.vercel.app")
                           .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader(); // لو بتستخدم كوكيز
                });
            });
  
                   


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAngularApp"); // Use CORS policy

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
