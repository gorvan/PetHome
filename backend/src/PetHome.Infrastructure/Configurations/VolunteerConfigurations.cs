﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Domain.Models.CommonModels;
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
                .HasMaxLength(Constants.MAX_WORD_LENGTH)
                .HasColumnName("first_name");
            
                tb.Property(n => n.SecondNname)
                .IsRequired(false)
                .HasMaxLength(Constants.MAX_WORD_LENGTH)
                .HasColumnName("second_name");
           
                tb.Property(n => n.Surname)
                .IsRequired(false)
                .HasMaxLength(Constants.MAX_WORD_LENGTH)
                .HasColumnName("surname");
            });

            builder.Property(v => v.DescriptionValue)
                .HasConversion(
                    id => id.Value,
                    value => Description.Create(value).Value)
                .IsRequired(false)
                .HasMaxLength(Constants.MAX_TEXT_LENGTH)
                .HasColumnName("description");

            builder.ComplexProperty(v => v.Email, tb =>
            {
                tb.Property(n => n.Value)
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

            builder.HasMany(v => v.Detailes)
                .WithOne()                
                .HasForeignKey("voluteer_id")
                .IsRequired(false);

            builder.OwnsOne(v => v.SocialNetworksValue, sb =>
            {
                sb.ToJson();
                sb.OwnsMany(s => s.SocialNetworks, nb =>
                {
                    nb.Property(sn => sn.Name)
                    .HasConversion(
                        s => s.Value,
                        value => NotNullableString.Create(value).Value)
                    .IsRequired(false)
                    .HasMaxLength(Constants.MAX_TITLE_LENGTH);

                    nb.Property(sn => sn.Link)
                    .HasConversion(
                        s => s.Value,
                        value => NotNullableText.Create(value).Value)
                    .IsRequired(false)
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
