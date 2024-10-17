using PetHome.Species.Application;
using PetHome.Species.Infrastructure;
using PetHome.Volunteers.Application;
using PetHome.Volunteers.Infrastructure;
using PetHome.Volunteers.Contracts;
using PetHome.Species.Contracts;
using PetHome.Web.Middleware;
using Serilog;
using PetHome.Volunteers.Presentation;
using PetHome.Species.Presentation;
using PetHome.Species.Presentation.Controllers;
using PetHome.Volunteers.Presentation.Controllers;

namespace PetHome.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureLogging();

            builder.Services.AddControllers()
            .AddApplicationPart(typeof(VolunteersController).Assembly)
            .AddApplicationPart(typeof(SpeciesController).Assembly);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSerilog();

            builder.Services
                .AddVolunteersApplication()
                .AddVolunteerInfrastructure(builder.Configuration)
                .AddSpeciesApplication()
                .AddSpeciesInfrastructure(builder.Configuration);

            builder.Services.AddScoped<IVolunteersContract, VolunteersContract>();
            builder.Services.AddScoped<ISpeciesContract, SpeciesContract>();

            var app = builder.Build();

            app.UseExceptionMiddleware();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                //await app.ApplyMigrations();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
