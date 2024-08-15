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
            builder.HasKey(x => x.Id);

            builder.Property(m => m.Id)
                .HasConversion(
                    id => id.Value,
                    value => RequisiteId.Create(value));

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH);
        }
    }
}
