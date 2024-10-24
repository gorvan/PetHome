using Microsoft.OpenApi.Models;
using PetHome.Accounts.Infrastructure;
using PetHome.Species.Application;
using PetHome.Species.Contracts;
using PetHome.Species.Infrastructure;
using PetHome.Species.Presentation;
using PetHome.Species.Presentation.Controllers;
using PetHome.Volunteers.Application;
using PetHome.Volunteers.Contracts;
using PetHome.Volunteers.Infrastructure;
using PetHome.Volunteers.Presentation;
using PetHome.Volunteers.Presentation.Controllers;
using PetHome.Web.Middleware;
using Serilog;

namespace PetHome.Web;

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

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
               {
                 new OpenApiSecurityScheme
                 {
                   Reference = new OpenApiReference
                   {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                   }
                  },
                  new string[] { }
                }
              });
        });

        builder.Services.AddSerilog();

        builder.Services
            .AddVolunteersApplication()
            .AddVolunteerInfrastructure(builder.Configuration)
            .AddSpeciesApplication()
            .AddSpeciesInfrastructure(builder.Configuration)
            .AddAccountsInfrastructure(builder.Configuration);

        builder.Services.AddScoped<IVolunteersContract, VolunteersContract>();
        builder.Services.AddScoped<ISpeciesContract, SpeciesContract>();

        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseExceptionMiddleware();
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            //await app.ApplyMigrations();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
