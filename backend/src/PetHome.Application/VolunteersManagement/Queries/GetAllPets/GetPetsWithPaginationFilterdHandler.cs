using PetHome.Application.Abstractions;
using PetHome.Application.Database;
using PetHome.Application.Dtos;
using PetHome.Application.Extensions;
using PetHome.Application.Models;
using PetHome.Domain.Shared;
using System.Linq.Expressions;

namespace PetHome.Application.VolunteersManagement.Queries.GetAllPets
{
    public class GetPetsWithPaginationFilterdHandler
        : IQueryHandler<Result<PagedList<PetDto>>, GetPetsWithPaginationFilteredQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetPetsWithPaginationFilterdHandler(IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<PagedList<PetDto>>> Execute(
            GetPetsWithPaginationFilteredQuery query,
            CancellationToken token)
        {
            var petQuery = _readDbContext.Pets;

            var filter = GetFilter(query);

            var keySelector = GetSelector(query);

            petQuery = query.SortDirection?.ToLower() == Constants.SORT_DESCENDING
                ? petQuery.OrderByDescending(keySelector)
                : petQuery.OrderBy(keySelector);

            petQuery = petQuery.Where(filter);

            return await petQuery
                    .ToPagedList(query.Page, query.PageSize, token);
        }

        private Expression<Func<PetDto, bool>> GetFilter(GetPetsWithPaginationFilteredQuery query)
        {
            return query.FilterBy?.ToLower() switch
            {
                "volunteerid" => (p) => p.VolunteerId == (Guid)query.FilterValue!,
                "nickname" => (p) => p.Nickname == (string)query.FilterValue!,
                "birthday" => (p) => p.BirthDay == (DateTime)query.FilterValue!,
                "speciesid" => (p) => p.SpeciesId == (Guid)query.FilterValue!,
                "breedid" => (p) => p.BreedId == (Guid)query.FilterValue!,
                "color" => (p) => p.Color == (string)query.FilterValue!,
                "health" => (p) => p.Health == (string)query.FilterValue!,
                "city" => (p) => p.City == (string)query.FilterValue!,
                "isneutered" => (p) => p.IsNeutered,
                "isvaccinated" => (p) => p.IsVaccinated,
                "helpstatus" => (p) => p.HelpStatus.ToString() == (string)query.FilterValue!,
                "weight" => (p) => p.Weight == (double)query.FilterValue!,
                "height" => (p) => p.Height == (double)query.FilterValue!,
                _ => (p) => p.Nickname == (string)query.FilterValue!
            };
        }

        private Expression<Func<PetDto, object>> GetSelector(GetPetsWithPaginationFilteredQuery query)
        {
            return query.SortBy?.ToLower() switch
            {
                "volunteerid" => (p) => p.VolunteerId,
                "nickname" => (p) => p.Nickname,
                "birthday" => (p) => p.BirthDay,
                "speciesid" => (p) => p.SpeciesId,
                "breedid" => (p) => p.BreedId,
                "color" => (p) => p.Color,
                "city" => (p) => p.City,
                "street" => (p) => p.Street,
                _ => (v) => v.VolunteerId,
            };
        }
    }
}
