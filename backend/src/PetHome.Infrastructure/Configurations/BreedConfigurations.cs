using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.Models.Pets;
using PetHome.Domain.Shared;

namespace PetHome.Infrastructure.Configurations
{
    public class BreedConfigurations : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable("breeds");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.Value,
                    value => BreedId.Create(value))
                .IsRequired();

            builder.ComplexProperty(v => v.Name, tb =>
            {
                tb.Property(n => n.Value)
                .IsRequired()
                .HasDefaultValue("Unknown")
                .HasMaxLength(Constants.MAX_WORD_LENGTH)
                .HasColumnName("breed");
            });           
        }
    }
}
