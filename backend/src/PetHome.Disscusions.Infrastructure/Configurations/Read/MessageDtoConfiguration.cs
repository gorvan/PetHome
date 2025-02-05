using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Shared.Core.Dtos;

namespace PetHome.Disscusions.Infrastructure.Configurations.Read;

public class MessageDtoConfiguration : IEntityTypeConfiguration<MessageDto>
{
    public void Configure(EntityTypeBuilder<MessageDto> builder)
    {
        builder.ToTable("messages");

        builder.HasKey(m => m.MessageId);
    }
}
