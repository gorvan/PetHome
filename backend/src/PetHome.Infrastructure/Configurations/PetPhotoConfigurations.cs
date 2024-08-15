using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.Models.Pets;
using PetHome.Domain.Shared;

namespace PetHome.Infrastructure.Configurations
{
    public class PetPhotoConfigurations : IEntityTypeConfiguration<PetPhoto>
    {
        public void Configure(EntityTypeBuilder<PetPhoto> builder)
        {
            builder.ToTable("pet_photo");
            builder.HasKey(x => x.Id);

            builder.Property(m => m.Id)
                .HasConversion(
                    id => id.Value,
                    value => PetPhotoId.Create(value));

            builder.Property(m => m.Title)
                .IsRequired(false)
                .HasMaxLength(Constants.MAX_TITLE_LENGTH);

            builder.Property(m => m.IsMain)
                .IsRequired();

            builder.Property(m => m.Path)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH);
        }
    }
}
