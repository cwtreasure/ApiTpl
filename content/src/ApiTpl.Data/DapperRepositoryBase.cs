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

        public async Task<List<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            var result = await db.QueryAsync<T>(sql, param, transaction, commandTimeout);
            return result.ToList();
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            return await db.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            return await db.ExecuteAsync(sql, param, transaction, commandTimeout);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = 5)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            return await db.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout);
        }

        public async Task<int> AddAsync(object data, string table, IDbTransaction transaction = null, int? commandTimeout = 5)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            return await db.AddAsync(data, table, transaction, commandTimeout);
        }

        public async Task<int> ModifyAsync(object data, object condition, string table, IDbTransaction transaction = null, int? commandTimeout = 5)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            return await db.ModifyAsync(data, condition, table, transaction, commandTimeout);
        }

        public async Task<int> ModifyNotNullAsync(object data, object condition, string table, IDbTransaction transaction = null, int? commandTimeout = 5)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            return await db.ModifyNotNullAsync(data, condition, table, transaction, commandTimeout);
        }

        public async Task<long> GetCountAsync(object condition, string table, IDbTransaction transaction = null, int? commandTimeout = 5)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            return await db.GetCountAsync(condition, table, transaction, commandTimeout);
        }

        public async Task<long> GetCountAsync(string where, object condition, string table, IDbTransaction transaction = null, int? commandTimeout = 5)
        {
            using var db = GetDbConnection();
            await db.OpenAsync();
            return await db.GetCountAsync(where, condition, table, transaction, commandTimeout);
        }
    }
}
