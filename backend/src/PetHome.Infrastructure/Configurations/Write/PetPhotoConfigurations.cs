using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Infrastructure.Configurations.Write
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

            builder.ComplexProperty(p => p.Path,
                pb =>
                {
                    pb.Property(x => x.Path)
                    .IsRequired()
                    .HasColumnName("path");
                });

            builder.Property(p => p.IsMain)
                .IsRequired()
                .HasColumnType("boolean")
                .HasColumnName("is_main");
        }
    }
}
