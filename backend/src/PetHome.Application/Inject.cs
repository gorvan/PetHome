using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHome.Application.Pets.Files.AddFiles;
using PetHome.Application.Pets.Files.Delete;
using PetHome.Application.Pets.Files.GetFile;
using PetHome.Application.Pets.Files.GetFiles;
using PetHome.Application.VolunteersManagement.Create;
using PetHome.Application.VolunteersManagement.Delete;
using PetHome.Application.VolunteersManagement.PetManagement.AddPet;
using PetHome.Application.VolunteersManagement.PetManagement.AddPetFiles;
using PetHome.Application.VolunteersManagement.Restore;
using PetHome.Application.VolunteersManagement.UpdateMainInfo;
using PetHome.Application.VolunteersManagement.UpdateRequisites;
using PetHome.Application.VolunteersManagement.UpdateSocialNetworks;

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
            services.AddScoped<AddPetHandler>();
            services.AddScoped<AddPetFilesHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
