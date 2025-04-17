using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperMvcDemo.Data.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "conn")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            return await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string spName, T parameters, string connectionId = "conn")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        // Optional: if you want to return auto-generated ID (e.g., from INSERT)
        public async Task<int> SaveDataWithReturn<T>(string spName, T parameters, string connectionId = "conn")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            return await connection.ExecuteScalarAsync<int>(spName, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
