using CalculatorProject.BusinessLogic;
using CalculatorProject.Contracts;
using CalculatorProject.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace CalculatorWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // register the repository you will use
            builder.Services.AddSingleton<IRepository, JsonRepositoryManager>();
            builder.Services.AddSingleton<IRepository, SQLRepositoryManager>();
            builder.Services.AddSingleton<IRepository, XmlRepositoryManager>();
            builder.Services.AddSingleton<Calculator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calculator API v1");
                    c.RoutePrefix = "";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

