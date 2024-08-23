using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Infrastructure.Configurations
{
    public class PetPhotoConfigurations : IEntityTypeConfiguration<PetPhoto>
    {
        public void Configure(EntityTypeBuilder<PetPhoto> builder)
        {
            builder.ToTable("pet_photos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Id,
                    value => PetPhotoId.Create(value));

            builder.Property(p => p.Path)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH)
                .HasColumnName("path");

            builder.Property(p => p.IsMain)
                .IsRequired()
                .HasColumnType("boolean")
                .HasColumnName("is_main");
        }
    }
}
