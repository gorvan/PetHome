
using PetHome.Application.Volunteers;
using PetHome.Application.Volunteers.CreateVolunteer;
using PetHome.Infrastructure;
using PetHome.Infrastructure.Repositories;

namespace PetHome.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllers();            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddScoped<IVolunteerRepository, VolunteersRepository>();
            builder.Services.AddScoped<CreateVolunteerHandler>();

            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();            

            app.Run();
        }
    }
}
