using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.Shared.Core.Dtos;

namespace PetHome.VolunteerRequests.Infrastructure.Configurations.Read;
public class VolunteerRequestDtoConfigurations : IEntityTypeConfiguration<VolunteerRequestDto>
{
    public void Configure(EntityTypeBuilder<VolunteerRequestDto> builder)
    {
        builder.ToTable("volunteer_requests");

        builder.HasKey(vr => vr.RequestId);

        builder.ComplexProperty(vr => vr.VolunteerInfo,
            ib =>
            {
                ib.ComplexProperty(e => e.FullName,
                    nb =>
                    {
                        nb.Property(n => n.FirstName);
                        nb.Property(n => n.SecondName);
                        nb.Property(n => n.Surname);
                    });
                ib.Property(e => e.Email);
                ib.Property(e => e.Description);
                ib.Property(e => e.Phone);
            });

    }
}
