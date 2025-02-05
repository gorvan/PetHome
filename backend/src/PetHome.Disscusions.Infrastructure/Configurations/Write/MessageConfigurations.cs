using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Disscusions.Domain;
using PetHome.Shared.Core.Shared.IDs;

namespace PetHome.Disscusions.Infrastructure.Configurations.Write;
public class MessageConfigurations : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");

        builder.HasKey(m => m.MessageId);

        builder.Property(m => m.MessageId)
            .HasConversion(
                id => id.Id,
                value => MessageId.Create(value))
            .HasColumnName("id");

        builder.Property(m => m.Text)
            .IsRequired()
            .HasColumnName("text");

        builder.Property(m => m.CreatedAt)
            .IsRequired()
            .HasColumnName("create_at");

        builder.Property(m => m.IsEdited)
            .IsRequired()
            .HasColumnName("is_edited");

        builder.Property(m => m.UserId)
            .IsRequired()
            .HasColumnName("user_id");
    }
}
