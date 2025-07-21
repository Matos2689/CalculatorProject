using System.Runtime.Intrinsics.X86;
using Azure.Core;
using CalculatorProject.BusinessLogic;
using CalculatorProject.Contracts;
using CalculatorProject.Persistance;
using static System.Net.Mime.MediaTypeNames;

namespace CalculatorWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register the repositories and business logic
            builder.Services.AddSingleton<IRepository, SQLRepositoryManager>();
            builder.Services.AddSingleton<Calculator>();

            /*
            Scoped:
            Isolated, new to each refinement.
            Use for short things (by Request).

            Singleton:
            Shared by all, from the beginning to the end of the application.
            Use when you want to share state, cache, or “global” features.
            */

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Middlewares
            app.UseHttpsRedirection();
            app.UseAuthorization();

            //app.MapGet("/", () => Results.Redirect("/swagger"));

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
