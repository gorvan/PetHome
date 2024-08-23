using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;

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
                id => id.Id,
                value => PetId.Create(value))
                .HasColumnName("pet_id");

            builder.ComplexProperty(p => p.Nickname,
                pb =>
                {
                    pb.Property(n => n.Nickname)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                    .HasColumnName("nickname");
                });

            builder.ComplexProperty(p => p.SpeciesBreed,
                pb =>
                {
                    pb.Property(s => s.SpeciesId)
                    .HasConversion(
                        id => id.Id,
                        value => SpeciesId.Create(value))
                    .IsRequired()
                    .HasColumnName("species_id");

                    pb.Property(s => s.BreedId)
                    .HasConversion(
                        id => id.Id,
                        value => BreedId.Create(value))
                    .IsRequired()
                    .HasColumnName("breed_id");
                });

            builder.ComplexProperty(p => p.Description,
                pb =>
                {
                    pb.Property(d => d.Description)
                    .IsRequired()
                    .HasColumnName("description");
                });

            builder.ComplexProperty(p => p.Color,
                pb =>
                {
                    pb.Property(c => c.Color)
                    .IsRequired()
                    .HasColumnName("color");
                });

            builder.ComplexProperty(p => p.Health,
                pb =>
                {
                    pb.Property(h => h.Health)
                    .IsRequired()
                    .HasColumnName("health");
                });

            builder.ComplexProperty(p => p.Address,
                pb =>
                {
                    pb.Property(a => a.City)
                    .IsRequired()
                    .HasColumnName("city");

                    pb.Property(a => a.Street)
                    .IsRequired()
                    .HasColumnName("street");

                    pb.Property(a => a.HouseNumber)
                    .IsRequired()
                    .HasColumnName("house");

                    pb.Property(a => a.AppartmentNumber)
                    .IsRequired()
                    .HasColumnName("appartment");
                });

            builder.ComplexProperty(p => p.Phone,
               pb =>
               {
                   pb.Property(p => p.PhoneNumber)
                   .IsRequired()
                   .HasColumnName("phone");
               });

            builder.OwnsOne(p => p.Requisites,
               pb =>
               {
                   pb.ToJson("requisites");

                   pb.OwnsMany(r => r.Requisites,
                       rb =>
                       {
                           rb.Property(i => i.Name)
                           .IsRequired()
                           .HasMaxLength(Constants.MAX_TITLE_LENGTH);

                           rb.Property(i => i.Description)
                          .IsRequired()
                          .HasMaxLength(Constants.MAX_TEXT_LENGTH);
                       });
               });


            builder.ComplexProperty(p => p.BirthDay,
               pb =>
               {
                   pb.Property(d => d.Date)
                   .IsRequired()
                   .HasColumnName("birthday");
               });

            builder.ComplexProperty(p => p.CreateDate,
               pb =>
               {
                   pb.Property(d => d.Date)
                   .IsRequired()
                   .HasColumnName("create_date");
               });

            builder.Property(p => p.IsNeutered)
                .IsRequired()
                .HasColumnName("is_neutered");

            builder.Property(p => p.IsVaccinated)
                .IsRequired()
                .HasColumnName("is_vaccinated");

            builder.Property(p => p.IsVaccinated)
                .IsRequired()
                .HasColumnName("is_vaccinated");

            builder.Property(p => p.HelpStatus)
                .IsRequired()
                .HasColumnName("help_status");

            builder.Property(p => p.Weight)
                .IsRequired()
                .HasColumnName("weight");

            builder.Property(p => p.Height)
                .IsRequired()
                .HasColumnName("height");

            builder.HasMany(p => p.Photos)
            .WithOne()
            .HasForeignKey("pet_id");
        }
    }
}
