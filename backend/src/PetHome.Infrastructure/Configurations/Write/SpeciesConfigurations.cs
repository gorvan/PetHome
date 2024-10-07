using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.Shared.IDs;
using PetHome.Domain.SpeciesManagement.Entities;

namespace PetHome.Infrastructure.Configurations.Write
{
    public class SpeciesConfigurations : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    id => id.Id,
                    value => SpeciesId.Create(value));

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_LENGTH)
                .HasColumnName("name");

            builder.HasMany(s => s.Breeds)
                .WithOne()
                .HasForeignKey("species_id");

            builder.Navigation(s => s.Breeds).AutoInclude();
        }
    }
}
