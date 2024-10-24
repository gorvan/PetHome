using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Shared.Core.Shared.IDs;
using PrtHome.Species.Domain.ValueObjects;

namespace PetHome.Species.Infrastructure.Configurations.Write
{
    public class BreedConfigurations : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable("breeds");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.Id,
                    value => BreedId.Create(value))
                .HasColumnName("breed_id");

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(Shared.Core.Shared.Constants.MAX_TITLE_LENGTH)
                .HasColumnName("name");
        }
    }
}
