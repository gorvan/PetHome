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
            builder.HasKey(x => x.Id);

            builder.Property(m => m.Id)
                .HasConversion(
                    id => id.Value,
                    value => VolunteerId.Create(value));

            builder.ComplexProperty(m => m.Name, tb =>
            {
                tb.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(Adress.MAX_LENGTH)
                .HasColumnName("first_name");
            });

            builder.ComplexProperty(m => m.Name, tb =>
            {
                tb.Property(t => t.SeconNname)
                .IsRequired()
                .HasMaxLength(Adress.MAX_LENGTH)
                .HasColumnName("second_name");
            });

            builder.ComplexProperty(m => m.Name, tb =>
            {
                tb.Property(t => t.Surname)
                .IsRequired()
                .HasColumnName("surname");
            });

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH);

            builder.Property(m => m.Experience)
                .IsRequired();

            builder.Property(m => m.FoundHomePets)
                .IsRequired();

            builder.Property(m => m.NeedHomePets)
                .IsRequired();

            builder.Property(m => m.TreatPets)
                .IsRequired();

            builder.Property(m => m.Phone)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                .HasColumnName("phone");

            builder.HasMany(m => m.Detailes)
                .WithOne()
                .HasForeignKey("voluteer_id");

            builder.OwnsOne(m => m.SocialNetworks, sb =>
            {
                sb.ToJson();
                sb.OwnsMany(s => s.SocialNetworks, nb =>
                {
                    nb.Property(n => n.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH);

                    nb.Property(n => n.Link)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH);
                });
            });

            builder.HasMany(m => m.Pets)
                .WithOne()
                .HasForeignKey("voluteer_id");
        }
    }
}
