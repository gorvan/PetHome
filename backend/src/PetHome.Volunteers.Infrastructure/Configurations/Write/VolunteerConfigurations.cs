using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Volunteers.Domain;
using PetHome.Volunteers.Domain.ValueObjects;
using System.Text.Json;

namespace PetHome.Volunteers.Infrastructure.Configurations.Write
{
    public class VolunteerConfigurations : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasConversion(
                    id => id.Id,
                    value => VolunteerId.Create(value))
                .IsRequired();

            builder.ComplexProperty(v => v.Name,
                vb =>
                {
                    vb.Property(n => n.FirstName)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("first_name");

                    vb.Property(n => n.SecondName)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("second_name");

                    vb.Property(n => n.Surname)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("surname");
                });

            builder.ComplexProperty(v => v.Email,
                vb =>
                {
                    vb.Property(e => e.EmailValue)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("email");
                });

            builder.ComplexProperty(v => v.Description,
                vb =>
                {
                    vb.Property(d => d.DescriptionValue)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TEXT_LENGTH)
                    .HasColumnName("description");
                });

            builder.ComplexProperty(v => v.Phone,
                vb =>
                {
                    vb.Property(p => p.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("phone");
                });

            builder.Property(p => p.SocialNetworks)
                .HasConversion(
                    sn => JsonSerializer.Serialize(
                        sn.Select(n =>
                            new SocialNetworkDto(n.Name, n.Link)),
                            JsonSerializerOptions.Default),
                    json => JsonSerializer.Deserialize<List<SocialNetworkDto>>(json, JsonSerializerOptions.Default)!
                        .Select(dto => SocialNetwork.Create(dto.Name, dto.Path).Value).ToList())
                .HasColumnName("social_networks");

            builder.Property(p => p.Requisites)
                .HasConversion(
                    r => JsonSerializer.Serialize(
                        r.Select(x =>
                            new RequisiteDto(x.Name, x.Description)),
                            JsonSerializerOptions.Default),
                    json => JsonSerializer.Deserialize<List<RequisiteDto>>(json, JsonSerializerOptions.Default)!
                        .Select(dto => Requisite.Create(dto.Name, dto.Description).Value).ToList())
                .HasColumnName("requisites");

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id");

            builder.Property(v => v.Experience)
                .IsRequired()
                .HasColumnName("experience");

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_delete");

            builder.Navigation(v => v.Pets).AutoInclude();
        }
    }
}
