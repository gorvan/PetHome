using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Application.Pets.AddFiles;
using PetHome.Application.Pets.Delete;
using PetHome.Application.Pets.GetFile;
using PetHome.Application.Pets.GetFiles;
using PetHome.Application.Volunteers.Create;
using PetHome.Application.Volunteers.Delete;
using PetHome.Application.Volunteers.Restore;
using PetHome.Application.Volunteers.UpdateMainInfo;
using PetHome.Application.Volunteers.UpdateRequisites;
using PetHome.Application.Volunteers.UpdateSocialNetworks;

namespace PetHome.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();
            services.AddScoped<UpdateMainInfoHandler>();
            services.AddScoped<DeleteVolunteerHandler>();
            services.AddScoped<RestoreVolunteerHandler>();
            services.AddScoped<UpdateRequisitesHandler>();
            services.AddScoped<UpdateSocialNetworksHandler>();
            services.AddScoped<AddFilesHandler>();
            services.AddScoped<GetFileHandler>();
            services.AddScoped<GetFilesHandler>();
            services.AddScoped<DeleteFileHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
