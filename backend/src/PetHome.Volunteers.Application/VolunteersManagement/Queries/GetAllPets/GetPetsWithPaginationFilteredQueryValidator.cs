﻿using FluentValidation;

namespace PetHome.Volunteers.Application.VolunteersManagement.Queries.GetAllPets
{
    public class GetPetsWithPaginationFilteredQueryValidator :
        AbstractValidator<GetPetsWithPaginationFilteredQuery>
    {
        public GetPetsWithPaginationFilteredQueryValidator()
        {
            RuleFor(p => p.FilterValue).NotNull();

            RuleFor(p => p.SortBy).NotNull();
        }
    }
}
