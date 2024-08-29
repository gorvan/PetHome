using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.PetManadgement.AggregateRoot;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

namespace PetHome.Infrastructure.Configurations
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

            builder.OwnsOne(v => v.SocialNets,
                vb =>
                {
                    vb.ToJson("social_networks");

                    vb.OwnsMany(s => s.Networks,
                        sb =>
                        {
                            sb.Property(s => s.Name);
                            sb.Property(s => s.Link);
                        });
                });

            builder.OwnsOne(v => v.Requisites,
                vb =>
                {
                    vb.ToJson("requisites");
                    vb.OwnsMany(r => r.Requisites,
                        rb =>
                        {
                            rb.Property(r => r.Name);
                            rb.Property(r => r.Description);
                        });

                });

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id");

            builder.Navigation(v => v.Pets).AutoInclude();
        }
    }
}
