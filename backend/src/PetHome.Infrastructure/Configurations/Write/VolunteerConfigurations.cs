using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.PetManadgement.AggregateRoot;
using PetHome.Domain.Shared.IDs;
using PetHome.Infrastructure.Extensions;

namespace PetHome.Infrastructure.Configurations.Write
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
                    .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("first_name");

                    vb.Property(n => n.SecondName)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("second_name");

                    vb.Property(n => n.Surname)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("surname");
                });

            builder.ComplexProperty(v => v.Email,
                vb =>
                {
                    vb.Property(e => e.EmailValue)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("email");
                });


            builder.ComplexProperty(v => v.Description,
                vb =>
                {
                    vb.Property(d => d.Description)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_TEXT_LENGTH)
                    .HasColumnName("description");
                });

            builder.ComplexProperty(v => v.Phone,
                vb =>
                {
                    vb.Property(p => p.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("phone");
                });

            builder.Property(v => v.SocialNetworks)
                .HasValueJsonConverter()
                .HasColumnName("social_networks");

            builder.Property(v => v.Requisites)
                .HasValueJsonConverter()
                .HasColumnName("requisites");

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id");

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_delete");

            builder.Navigation(v => v.Pets).AutoInclude();
        }
    }
}
