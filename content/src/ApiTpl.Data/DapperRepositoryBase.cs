namespace ApiTpl.Data
{
    using Dapper;
#if MsSQL
    using Microsoft.Data.SqlClient;
#endif
#if SQLite
    using Microsoft.Data.Sqlite;
#endif
    using Microsoft.Extensions.Configuration;
#if MySQL
    using MySql.Data.MySqlClient;
#endif
#if PgSQL
    using Npgsql;
#endif
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
#if SQLite
            return new SqliteConnection(_connStr);
#elif MySQL
            return new MySqlConnection(_connStr);
#elif PgSQL
            return new NpgsqlConnection(_connStr);
#elif MsSQL
            return new SqlConnection(_connStr);
#else
            throw new System.ArgumentNullException("");
#endif
        }

        protected DbConnection GetDbConnection(string connStr)
        {
#if SQLite
            return new SqliteConnection(connStr);
#elif MySQL
            return new MySqlConnection(connStr);
#elif PgSQL
            return new NpgsqlConnection(connStr);
#elif MsSQL
            return new SqlConnection(connStr);
#else
            throw new System.ArgumentNullException("");
#endif
        }

        public async Task<List<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5, CommandType? commandType = null)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            var result = await db.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
            return result.ToList();
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5, CommandType? commandType = null)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            return await db.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5, CommandType? commandType = null)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            return await db.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5, CommandType? commandType = null)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            return await db.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
