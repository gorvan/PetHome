using Microsoft.OpenApi.Models;
using PetHome.Accounts.Application;
using PetHome.Accounts.Infrastructure;
using PetHome.Accounts.Infrastructure.Extensions;
using PetHome.Accounts.Presentation.Controllers;
using PetHome.Disscusions.Application;
using PetHome.Disscusions.Contracts;
using PetHome.Disscusions.Infrastructure;
using PetHome.Disscusions.Presentation;
using PetHome.Disscusions.Presentation.Controllers;
using PetHome.Shared.Core.Extensions;
using PetHome.Species.Application;
using PetHome.Species.Contracts;
using PetHome.Species.Infrastructure;
using PetHome.Species.Presentation;
using PetHome.Species.Presentation.Controllers;
using PetHome.VolunteerRequests.Application;
using PetHome.VolunteerRequests.Infrastructure;
using PetHome.VolunteerRequests.Presentation.Controllers;
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
    public static async Task Main(string[] args)
    {
        DotNetEnv.Env.Load();

        var builder = WebApplication.CreateBuilder(args);

        builder.ConfigureLogging();

        builder.Services.AddControllers()
        .AddApplicationPart(typeof(VolunteersController).Assembly)
        .AddApplicationPart(typeof(SpeciesController).Assembly)
        .AddApplicationPart(typeof(AccountsController).Assembly)
        .AddApplicationPart(typeof(DisscusionConrtoller).Assembly)
        .AddApplicationPart(typeof(VolunteerRequestController).Assembly);

        builder.Services.AddEndpointsApiExplorer();
       
        builder.Services.AddSwaggerGen(c =>
        {            
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MyAPI",
                Version = "v1"                
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
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
            .AddAccountsApplication()
            .AddAccountsInfrastructure(builder.Configuration)
            .AddVolunteersApplication()
            .AddVolunteerInfrastructure(builder.Configuration)
            .AddSpeciesApplication()
            .AddSpeciesInfrastructure(builder.Configuration)
            .AddDisscusionApplication()
            .AddDisscusionInfrastructure(builder.Configuration)
            .AddVolunteerRequestApplication()
            .AddVolunteerRequestInfrastructure();

        builder.Services.AddScoped<IVolunteersContract, VolunteersContract>();
        builder.Services.AddScoped<ISpeciesContract, SpeciesContract>();
        builder.Services.AddScoped<IDisscusionContract, DisscusionContract>();

        var app = builder.Build();

        app.UseExceptionMiddleware();
        app.UseSerilogRequestLogging();
       
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            await app.ApplyMigrations<AccountsDbContext>();
            await app.ApplyMigrations<Species.Infrastructure.DbContexts.WriteDbContext>();
            await app.ApplyMigrations<Volunteers.Infrastructure.DbContexts.WriteDbContext>();
            await app.ApplyMigrations<Disscusions.Infrastructure.DbContexts.WriteDbContext>();
        }

        await app.SeedAccountsData();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
