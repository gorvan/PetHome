using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Volunteers.Infrastructure.Configurations.Read
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
