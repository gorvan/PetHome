using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Application.Dtos;
using PetHome.Domain.PetManadgement.Entities;
using PetHome.Domain.Shared;
using PetHome.Domain.Shared.IDs;
using System.Text.Json;

namespace PetHome.Infrastructure.Configurations.Write
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
                    .HasMaxLength(Domain.Shared.Constants.MAX_TITLE_LENGTH)
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

            builder.Property(p => p.Requisites)
                .HasConversion(
                    r => JsonSerializer.Serialize(
                        r.Select(x => new RequisiteDto(x.Name, x.Description)), JsonSerializerOptions.Default),
                    json => JsonSerializer.Deserialize<List<RequisiteDto>>(json, JsonSerializerOptions.Default)!
                        .Select(dto => Requisite.Create(dto.Name, dto.Description).Value).ToList())
                .HasColumnName("requisites");

            builder.ComplexProperty(p => p.BirthDay,
               pb =>
               {
                   pb.Property(d => d.Date)
                   .IsRequired()
                   .HasColumnName("birthday");
               });

            builder.ComplexProperty(p => p.SerialNumber,
               pb =>
               {
                   pb.Property(s => s.Value)
                   .IsRequired()
                   .HasColumnName("serial_number");
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

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_delete");
        }
    }
}
