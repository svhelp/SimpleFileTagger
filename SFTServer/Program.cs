using Core.Commands.Locations;
using DAL;
using Microsoft.AspNetCore.Http.Features;
using SFTServer.Startup;

namespace SFTServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DatabaseInitializer.Init();

            WebApplication app = CreateApp(args);
            
            VerifyDbState(app);

            RunApp(app);
        }

        private static void VerifyDbState(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var verifyLocationsCommand = scope.ServiceProvider.GetService(typeof(VerifyLocationsCommand));
            (verifyLocationsCommand as VerifyLocationsCommand).Run(new Contracts.CommandModels.EmptyCommandModel());
        }

        private static void RunApp(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            app.UseAuthorization();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.MapControllers();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.Run();
        }

        private static WebApplication CreateApp(string[] args)
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

            return builder.Build();
        }
    }
}