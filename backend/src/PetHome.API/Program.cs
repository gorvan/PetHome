using PetHome.API.Extensions;
using PetHome.API.Middleware;
using PetHome.Application;
using PetHome.Infrastructure;
using Serilog;

namespace PetHome.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSerilog();

            builder.Services
                .AddInfrastructure(builder.Configuration)
                .AddApplication();

            var app = builder.Build();

            app.UseExceptionMiddleware();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                await app.ApplyMigrations();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
