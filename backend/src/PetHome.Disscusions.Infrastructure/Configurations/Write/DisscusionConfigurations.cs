using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Disscusions.Domain;
using PetHome.Shared.Core.Shared.IDs;
using System.Text.Json;

namespace PetHome.Disscusions.Infrastructure.Configurations.Write;
public class DisscusionConfigurations : IEntityTypeConfiguration<Disscusion>
{
    public void Configure(EntityTypeBuilder<Disscusion> builder)
    {
        builder.ToTable("disscusions");

        builder.HasKey(d => d.DisscusionId);

        builder.Property(d => d.DisscusionId)
            .HasConversion(
                id => id.Id,
                value => DisscusionId.Create(value))
            .HasColumnName("id");

        builder.Property(d => d.RelationId)
            .IsRequired()
            .HasColumnName("relation_id");

        builder.Property(d => d.State)
            .IsRequired()
            .HasColumnName("state");

        builder.Property(d => d.Users)
            .HasConversion(
                u => JsonSerializer.Serialize(u, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<IReadOnlyList<Guid>>(json, JsonSerializerOptions.Default)!)
            .IsRequired()
            .HasColumnName("users");

        builder.HasMany(d => d.Messages)
            .WithOne()
            .HasForeignKey("disscusion_id");

        builder.Navigation(d => d.Messages)
            .AutoInclude();
    }
}
