﻿using Dapper;
using PetHome.Application.Abstractions;
using PetHome.Application.Database;
using PetHome.Application.Dtos;
using PetHome.Application.Models;
using PetHome.Application.VolunteersManagement.Queries.GetVolunteersWithPagination;
using PetHome.Domain.Shared;
using System.Text.Json;

namespace PetHome.Application.VolunteersManagement.Queries.GetVolunteersWithPaginationDapper
{
    public class GetVolunteersWithPaginationFilteredDapperHandler
        : IQueryHandler<Result<PagedList<VolunteerDto>>, GetVolunteersWithPaginationFilteredQuery>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetVolunteersWithPaginationFilteredDapperHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<PagedList<VolunteerDto>>> Execute(
            GetVolunteersWithPaginationFilteredQuery query,
            CancellationToken token)
        {
            var connection = _sqlConnectionFactory.Create();
            var parameters = new DynamicParameters();

            var totalCount = await connection
                .ExecuteScalarAsync<long>("SELECT COUNT(*) FROM volunteers");

            parameters.Add("@PageSize", query.PageSize);
            parameters.Add("@Offset", (query.Page - 1) * query.PageSize);

            var sql = """
                SELECT id, email, phone, first_name, experience, requisites, social_networks FROM volunteers
                ORDER BY first_name LIMIT @PageSize OFFSET @Offset 
                """;

            var volunteers = await connection
                .QueryAsync<VolunteerDto, string, string, VolunteerDto>(
                sql,
                (volunteer, jsonRequisited, jsonSocialNetworks) =>
                {
                    var requisites = JsonSerializer.Deserialize<RequisiteDto[]>(jsonRequisited) ?? [];
                    var socialNetworks = JsonSerializer.Deserialize<SocialNetworkDto[]>(jsonSocialNetworks) ?? [];
                    volunteer.Requisites = requisites;
                    volunteer.SocialNetworks = socialNetworks;
                    return volunteer;
                },
                splitOn: "requisites, social_networks",
                param: parameters
                );

            return new PagedList<VolunteerDto>
            {
                Items = volunteers.ToList(),
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = totalCount
            };
        }
    }
}