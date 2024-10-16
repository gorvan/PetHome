﻿using Microsoft.Extensions.Configuration;
using Npgsql;
using PetHome.Shared.Core.Database;
using System.Data;

namespace PetHome.Shared.Framework
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IConfiguration _configuration;
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Create() =>
            new NpgsqlConnection(_configuration.GetConnectionString(Constants.DATABASE));
    }
}
