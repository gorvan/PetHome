using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.Models.Pets;
using PetHome.Domain.Shared;

namespace PetHome.Infrastructure.Configurations
{
    public class SpeciesConfigurations : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value));  
           
            builder.ComplexProperty(v => v.Name, tb =>
            {
                tb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                .HasColumnName("name");
            });

            builder.HasMany(s => s.Breeds)
                .WithOne()
                .HasForeignKey("species_id");
        }
    }
}
