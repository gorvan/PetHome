using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Application.Dtos;
using System.Text.Json;

namespace PetHome.Infrastructure.Configurations.Read
{
    public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
    {
        public void Configure(EntityTypeBuilder<VolunteerDto> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Requisites)
                .HasConversion(
                    r => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                    json => JsonSerializer.Deserialize<RequisiteDto[]>(json, JsonSerializerOptions.Default)!);

            builder.Property(v => v.SocialNetworks)
                .HasConversion(
                    s => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                    value => JsonSerializer.Deserialize<SocialNetworkDto[]>(value, JsonSerializerOptions.Default)!);

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey(i => i.VolunteerId);
        }
    }
}
