using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Application.Dtos;

namespace PetHome.Infrastructure.Configurations.Read
{
    public class PhotoDtoConfiguration : IEntityTypeConfiguration<PhotoDto>
    {
        public void Configure(EntityTypeBuilder<PhotoDto> builder)
        {
            builder.ToTable("pet_photos");

            builder.HasKey(p => p.Id);
        }
    }
}
