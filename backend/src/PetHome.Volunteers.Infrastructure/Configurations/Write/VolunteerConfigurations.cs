using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.Volunteers.Domain;

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
                    vb.Property(d => d.Description)
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

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id");

            builder.Property(v => v.Experience)
                .IsRequired()
                .HasColumnName("experience");

            builder.Navigation(v => v.Pets).AutoInclude();
        }
    }
}
