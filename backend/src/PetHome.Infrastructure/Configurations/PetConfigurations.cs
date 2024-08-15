using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.Models.Pets;
using PetHome.Domain.Shared;

namespace PetHome.Infrastructure.Configurations
{
    public class PetConfigurations : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");
            builder.HasKey(x => x.Id);

            builder.Property(m => m.Id)
                .HasConversion(
                    id => id.Value,
                    value => PetId.Create(value));

            builder.Property(m => m.Nickname)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH);

            builder.Property(m => m.Species)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH);

            builder.Property(m => m.Breed)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH);

            builder.Property(m => m.Color)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH);

            builder.Property(m => m.Health)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH);

            builder.ComplexProperty(m => m.Adress, tb =>
            {
                tb.Property(t => t.City)
                .IsRequired()
                .HasMaxLength(Adress.MAX_LENGTH)
                .HasColumnName("city");
            });

            builder.ComplexProperty(m => m.Adress, tb =>
            {
                tb.Property(t => t.Street)
                .IsRequired()
                .HasMaxLength(Adress.MAX_LENGTH)
                .HasColumnName("street");
            });

            builder.ComplexProperty(m => m.Adress, tb =>
            {
                tb.Property(t => t.HouseNumber)
                .IsRequired()                
                .HasColumnName("house");
            });

            builder.ComplexProperty(m => m.Adress, tb =>
            {
                tb.Property(t => t.AppartmentNumber)
                .IsRequired()                
                .HasColumnName("appartment");
            });

            builder.Property(m => m.Weight)
                .IsRequired()
                .HasColumnName("weight");


            builder.Property(m => m.Height)
                .IsRequired()
                .HasColumnName("height");

            builder.Property(m => m.Phone)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                .HasColumnName("phone");

            builder.Property(m => m.IsNeutered)
                .IsRequired()
                .HasColumnName("is_neutered");

            builder.Property(m => m.BirthDay)
                .IsRequired()
                .HasColumnName("birthday");

            builder.Property(m => m.IsVaccinated)
                .IsRequired()
                .HasColumnName("is_vaccinated");

            builder.Property(m => m.HelpStatus)
                .IsRequired()
                .HasColumnName("help_status");

            builder.Property(m => m.CreateTime)
                .IsRequired()
                .HasColumnName("create_time");

            builder.HasMany(m => m.Detailes)
                .WithOne()
                .HasForeignKey("pet_id");

            builder.HasMany(m => m.Photos)
                .WithOne()
                .HasForeignKey("pet_id");
        }
    }
}
