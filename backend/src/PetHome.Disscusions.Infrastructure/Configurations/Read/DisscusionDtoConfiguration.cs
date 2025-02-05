using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Shared.Core.Dtos;
using System.Text.Json;

namespace PetHome.Disscusions.Infrastructure.Configurations.Read;
public class DisscusionDtoConfiguration : IEntityTypeConfiguration<DisscusionDto>
{
    public void Configure(EntityTypeBuilder<DisscusionDto> builder)
    {
        builder.ToTable("disscusions");

        builder.HasKey(d => d.DisscusionId);

        builder.Property(d => d.Users)
             .HasConversion(
                 u => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                 json => JsonSerializer.Deserialize<Guid[]>(json, JsonSerializerOptions.Default)!);

        builder.HasMany(d => d.Messages)
            .WithOne()
            .HasForeignKey("disscusion_id");
    }
}
