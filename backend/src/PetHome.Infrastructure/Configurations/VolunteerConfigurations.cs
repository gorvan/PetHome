using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.Models.Pets;
using PetHome.Domain.Models.Volunteers;
using PetHome.Domain.Shared;

namespace PetHome.Infrastructure.Configurations
{
    public class VolunteerConfigurations : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteer");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasConversion(
                    id => id.Value,
                    value => VolunteerId.Create(value));

            builder.ComplexProperty(v => v.Name, tb =>
            {
                tb.Property(n => n.FirstName)
                .IsRequired()
                .HasMaxLength(Address.MAX_LENGTH)
                .HasColumnName("first_name");
            });

            builder.ComplexProperty(v => v.Name, tb =>
            {
                tb.Property(n => n.SeconNname)
                .IsRequired()
                .HasMaxLength(Address.MAX_LENGTH)
                .HasColumnName("second_name");
            });

            builder.ComplexProperty(v => v.Name, tb =>
            {
                tb.Property(n => n.Surname)
                .IsRequired()
                .HasColumnName("surname");
            });

            builder.Property(v => v.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH);

            builder.Property(v => v.Experience)
                .IsRequired();

            builder.Property(v => v.FoundHomePets)
                .IsRequired();

            builder.Property(v => v.NeedHomePets)
                .IsRequired();

            builder.Property(v => v.TreatPets)
                .IsRequired();

            builder.Property(v => v.Phone)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                .HasColumnName("phone");

            builder.HasMany(v => v.Detailes)
                .WithOne()
                .HasForeignKey("voluteer_id");

            builder.OwnsOne(v => v.SocialNetworks, sb =>
            {
                sb.ToJson();
                sb.OwnsMany(v => v.SocialNetworks, nb =>
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
                .HasForeignKey("voluteer_id");
        }
    }
}
