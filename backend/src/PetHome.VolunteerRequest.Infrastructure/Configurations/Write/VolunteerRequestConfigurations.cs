using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Shared.Core.Shared;
using PetHome.Shared.Core.Shared.IDs;
using PetHome.VolunteerRequests.Domain;
using PetHome.VolunteerRequests.Domain.ValueObjects;

namespace PetHome.VolunteerRequests.Infrastructure.Configurations.Write;
public class VolunteerRequestConfigurations : IEntityTypeConfiguration<VolunteerRequest>
{
    public void Configure(EntityTypeBuilder<VolunteerRequest> builder)
    {
        builder.ToTable("volunteer_requests");

        builder.HasKey(vr => vr.RequestId);

        builder.Property(vr => vr.RequestId)
            .HasConversion(
                id => id.Id,
                value => RequestId.Create(value))
            .HasColumnName("id");

        builder.Property(vr => vr.AdminId)
            .HasConversion(
                id => id.Id,
                value => AdminId.Create(value))
            .HasColumnName("admin_id");

        builder.Property(vr => vr.UserId)
            .HasConversion(
                id => id.Id,
                value => UserId.Create(value))
            .HasColumnName("user_id");

        builder.Property(vr => vr.DisscusionId)
            .HasConversion(
                id => id.Id,
                value => DisscusionId.Create(value))
            .HasColumnName("disscusionId_id");

        builder.ComplexProperty(vr => vr.VolunteerInfo,
            vrBuider =>
            {
                vrBuider.ComplexProperty(c => c.FullName,
                    fnBuilder =>
                    {
                        fnBuilder.Property(fn => fn.FirstName)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                        .HasColumnName("first_name");

                        fnBuilder.Property(fn => fn.SecondName)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                        .HasColumnName("second_name");

                        fnBuilder.Property(fn => fn.Surname)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_TITLE_LENGTH)
                        .HasColumnName("surname");
                    });

                vrBuider.ComplexProperty(c => c.Phone,
                    phBuider =>
                    {
                        phBuider.Property(ph => ph.PhoneNumber)
                        .IsRequired()
                        .HasColumnName("phone");
                    });

                vrBuider.ComplexProperty(c => c.Email,
                    eBuilder =>
                    {
                        eBuilder.Property(e => e.EmailValue)
                        .IsRequired()
                        .HasColumnName("email");
                    });

                vrBuider.ComplexProperty(c => c.Description,
                    dBuilder =>
                    {
                        dBuilder.Property(d => d.Description)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_TEXT_LENGTH)
                        .HasColumnName("description");
                    });
            });

        builder.Property(vr => vr.Status)
            .IsRequired()
            .HasColumnName("status");

        builder.ComplexProperty(vr => vr.CreatedAt,
            cBuilder =>
            {
                cBuilder.Property(c => c.Date)
                .IsRequired()
                .HasColumnName("created_at");
            });            

        builder.ComplexProperty(vr => vr.RejectionComment,
            rBuilder =>
            {
                rBuilder.Property(r => r.Value)
                .HasColumnName("reject_comment");
            });
    }
}
