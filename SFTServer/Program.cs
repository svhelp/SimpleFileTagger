using DAL;
using Microsoft.AspNetCore.Http.Features;
using SFTServer.Startup;

namespace SFTServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddContext()
                .AddSwagger()
                .AddMapper()
                .AddQueries()
                .AddCommands()
                .AddControllers();

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 15 * 1024 * 1024; //not recommended value
            });

            DatabaseInitializer.Init();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.MapControllers();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.Run();
        }
    }
}