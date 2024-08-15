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
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => PetId.Create(value));

            builder.Property(p => p.Nickname)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH);

            builder.Property(p => p.Species)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH);

            builder.Property(p => p.Breed)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH);

            builder.Property(p => p.Color)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH);

            builder.Property(p => p.Health)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TEXT_LENGTH);

            builder.ComplexProperty(p => p.Address, tb =>
            {
                tb.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Address.MAX_LENGTH)
                .HasColumnName("city");
            });

            builder.ComplexProperty(p => p.Address, tb =>
            {
                tb.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Address.MAX_LENGTH)
                .HasColumnName("street");
            });

            builder.ComplexProperty(p => p.Address, tb =>
            {
                tb.Property(a => a.HouseNumber)
                .IsRequired()                
                .HasColumnName("house");
            });

            builder.ComplexProperty(p => p.Address, tb =>
            {
                tb.Property(a => a.AppartmentNumber)
                .IsRequired()                
                .HasColumnName("appartment");
            });

            builder.Property(p => p.Weight)
                .IsRequired()
                .HasColumnName("weight");


            builder.Property(p => p.Height)
                .IsRequired()
                .HasColumnName("height");

            builder.Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                .HasColumnName("phone");

            builder.Property(p => p.IsNeutered)
                .IsRequired()
                .HasColumnName("is_neutered");

            builder.Property(p => p.BirthDay)
                .IsRequired()
                .HasColumnName("birthday");

            builder.Property(p => p.IsVaccinated)
                .IsRequired()
                .HasColumnName("is_vaccinated");

            builder.Property(p => p.HelpStatus)
                .IsRequired()
                .HasColumnName("help_status");

            builder.Property(p => p.CreateTime)
                .IsRequired()
                .HasColumnName("create_time");

            builder.HasMany(p => p.Detailes)
                .WithOne()
                .HasForeignKey("pet_id");

            builder.HasMany(p => p.Photos)
                .WithOne()
                .HasForeignKey("pet_id");
        }
    }
}
