using PetHome.Shared.Core.Abstractions;
using PetHome.Shared.Core.Dtos;
using PetHome.Shared.Core.Shared;

namespace PetHome.VolunteerRequests.Application.VolunteerRequestManagement.Queries.GetVolunteerRequestForReview;
public class GetVolunteerRequestForReviewHandler
    : IQueryHandler<Result<PetDto>, GetVolunteerRequestForReviewQuery>
{
    public Task<Result<PetDto>> Execute(GetVolunteerRequestForReviewQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
