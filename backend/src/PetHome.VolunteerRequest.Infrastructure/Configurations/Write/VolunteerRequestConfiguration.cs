using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHome.VolunteerRequests.Domain;

namespace PetHome.VolunteerRequests.Infrastructure.Configurations.Write;
public class VolunteerRequestConfiguration : IEntityTypeConfiguration<VolunteerRequest>
{
    public void Configure(EntityTypeBuilder<VolunteerRequest> builder)
    {
        builder.ToTable("volunteer_requests");

        builder.HasKey(vr=>vr.RequestId);


    }
}
