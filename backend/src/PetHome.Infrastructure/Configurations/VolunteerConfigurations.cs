using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.Models.Volunteers;
using PetHome.Domain.Shared;

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
                    id => id.Value,
                    value => VolunteerId.Create(value));

            builder.ComplexProperty(v => v.Name, tb =>
            {
                tb.Property(n => n.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_WORD_LENGTH)
                .HasColumnName("first_name");

                tb.Property(n => n.SecondName)
                .IsRequired()
                .HasMaxLength(Constants.MAX_WORD_LENGTH)
                .HasColumnName("second_name");

                tb.Property(n => n.Surname)
                .IsRequired()
                .HasMaxLength(Constants.MAX_WORD_LENGTH)
                .HasColumnName("surname");
            });

            builder.ComplexProperty(v => v.DescriptionValue, tb =>
            {
                tb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH)
                .HasColumnName("description");
            });

            builder.ComplexProperty(v => v.Email, tb =>
            {
                tb.Property(n => n.EmailValue)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                .HasColumnName("email");
            });

            builder.Property(v => v.Experience)
                .HasDefaultValue(0)
                .HasColumnName("experience");

            builder.ComplexProperty(v => v.Phone, tb =>
            {
                tb.Property(n => n.PhoneNumber)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                .HasColumnName("phone");
            });

            builder.OwnsOne(n => n.RequisiteCollectionValue, reqb =>
            {
                reqb.ToJson("requisite");
                reqb.OwnsMany(r => r.CollectionValues, rb =>
                {
                    rb.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH);

                    rb.Property(c => c.DescriptionValue)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TEXT_LENGTH);
                });
            });

            builder.OwnsOne(v => v.SocialNetworksValue, sb =>
            {
                sb.ToJson("social_networks");

                sb.OwnsMany(s => s.SocialNetworks, nb =>
                {
                    nb.Property(sn => sn.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH);

                    nb.Property(sn => sn.Link)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH);
                });
            });

            builder.HasMany(v => v.Pets)
                .WithOne()
                .IsRequired(false)
                .HasForeignKey("voluteer_id");
        }
    }
}
