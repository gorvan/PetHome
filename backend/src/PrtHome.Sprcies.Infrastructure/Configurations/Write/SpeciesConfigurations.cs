using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Species.Domain;

namespace PetHome.Species.Infrastructure.Configurations.Write
{
    public class SpeciesConfigurations : IEntityTypeConfiguration<SpeciesType>
    {
        public void Configure(EntityTypeBuilder<SpeciesType> builder)
        {
            builder.ToTable("species");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    id => id.Id,
                    value => SpeciesId.Create(value));

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(Shared.Core.Shared.Constants.MAX_TITLE_LENGTH)
                .HasColumnName("name");

            builder.HasMany(s => s.Breeds)
                .WithOne()
                .HasForeignKey("species_id");

            builder.Navigation(s => s.Breeds).AutoInclude();
        }
    }
}
