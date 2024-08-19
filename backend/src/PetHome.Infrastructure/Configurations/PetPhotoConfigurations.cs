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
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => PetPhotoId.Create(value));

            builder.Property(p => p.Title)
                .IsRequired(false)
                .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                .HasColumnName("title");

            builder.Property(p => p.IsMain)
                .IsRequired()
                .HasColumnName("is_main");

            builder.Property(p => p.Path)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH)
                .HasColumnName("path");
        }
    }
}
