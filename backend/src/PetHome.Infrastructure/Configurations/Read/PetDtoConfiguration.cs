using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Application.Dtos;
using PetHome.Infrastructure.Extensions;

namespace PetHome.Infrastructure.Configurations.Read
{
    public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
    {
        public void Configure(EntityTypeBuilder<PetDto> builder)
        {
            builder.ToTable("pets");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Photos)
                .HasValueJsonConverter();
        }
    }
}
