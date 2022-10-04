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