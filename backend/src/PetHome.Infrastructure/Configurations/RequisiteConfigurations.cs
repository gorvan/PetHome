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

            builder.ComplexProperty(p => p.Name, 
                tb =>
                {
                    tb.Property(a => a.Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("name");
                });

            builder.Property(v => v.DescriptionValue)
                .HasConversion(
                    id => id.Value,
                    value => Description.Create(value).Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH)
                .HasColumnName("description");            
        }
    }
}
