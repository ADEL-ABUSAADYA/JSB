
using JSB.BL.Interfaces;
using JSB.BL.Repositories;
using JSB.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System;

namespace JSB.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            builder.Services.AddControllers();

            // Configure the database connection (using SQL Server in this example)
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories for dependency injection
            builder.Services.AddScoped<IProductRep, ProductRep>();
            builder.Services.AddScoped<IOrderRep, OrderRep>();

            // Enable OpenAPI (Swagger) to help with testing the API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
