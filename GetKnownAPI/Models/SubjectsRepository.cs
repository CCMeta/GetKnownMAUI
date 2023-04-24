﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetKnownAPI.Models
{
    public class SubjectsRepository : BaseRepository
    {
        public SubjectsRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<Subjects>> GetList(int maxId = 0, int limit = 5)
        {
            var sql = "SELECT * FROM subjects WHERE id > @maxId LIMIT @limit";
            return await WithConnection(async conn =>
            {
                return await conn.QueryAsync<Subjects>(sql, new { maxId, limit });
            });

        }
    }
}
