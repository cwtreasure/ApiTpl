namespace ApiTpl.Data
{
    using Dapper;
    using Microsoft.Extensions.Configuration;
    using MySql.Data.MySqlClient;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;

    public class DapperRepositoryBase
    {
        protected readonly string _connStr;

        public DapperRepositoryBase(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("Default");
        }

        protected DbConnection GetDbConnection()
        {
            return new MySqlConnection(_connStr);
        }

        protected DbConnection GetDbConnection(string connStr)
        {
            return new MySqlConnection(connStr);
        }

        public async Task<List<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5, CommandType? commandType = null)
        {
            using (var db = GetDbConnection())
            {
                await db.OpenAsync();
                var result = await db.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
                return result.ToList();
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5, CommandType? commandType = null)
        {
            using (var db = GetDbConnection())
            {
                await db.OpenAsync();
                return await db.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5, CommandType? commandType = null)
        {
            using (var db = GetDbConnection())
            {
                await db.OpenAsync();
                return await db.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5, CommandType? commandType = null)
        {
            using (var db = GetDbConnection())
            {
                await db.OpenAsync();
                return await db.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }
    }
}
