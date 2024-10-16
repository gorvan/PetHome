﻿using FluentValidation;

namespace PetHome.Volunteers.Application.VolunteersManagement.Queries.GetVolunteersWithPagination
{
    public class GetVolunteersWithPaginationFilteredValidator
        : AbstractValidator<GetVolunteersWithPaginationFilteredQuery>
    {
        public GetVolunteersWithPaginationFilteredValidator()
        {
            RuleFor(g => g.Experience).GreaterThan(0).When(e => e != null);
        }
    }
}
