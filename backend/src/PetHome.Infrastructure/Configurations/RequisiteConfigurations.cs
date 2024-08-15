using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetHome.Domain.Shared;
using PetHome.Domain.Models.CommonModels;

namespace PetHome.Infrastructure.Configurations
{
    public class RequisiteConfigurations : IEntityTypeConfiguration<Requisite>
    {
        public void Configure(EntityTypeBuilder<Requisite> builder)
        {
            builder.ToTable("requisite");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasConversion(
                    id => id.Value,
                    value => RequisiteId.Create(value));

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH);

            builder.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH);
        }
    }
}
