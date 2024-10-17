using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Species.Infrastructure.Configurations.Read
{
    public class SpeciesDtoConfiguration : IEntityTypeConfiguration<SpeciesDto>
    {
        public void Configure(EntityTypeBuilder<SpeciesDto> builder)
        {
            builder.ToTable("species");

            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Breeds)
                .WithOne()
                .HasForeignKey(i => i.SpeciesId);
        }
    }
}
