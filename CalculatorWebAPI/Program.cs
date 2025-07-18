using CalculatorProject.BusinessLogic;
using CalculatorProject.Contracts;
using CalculatorProject.Persistance;

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
