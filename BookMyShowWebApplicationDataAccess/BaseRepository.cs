﻿using BookMyShowWebApplicationModal.config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;


namespace BookMyShowWebApplicationDataAccess
{
    
        public class BaseRepository: IBaseRepository
    {
            public readonly IOptions<DataConfig> _ConnectionString;
            public readonly IConfiguration configuration;
            private SqlConnection Conn;

        #region constructor

        public BaseRepository(IOptions<DataConfig> connectionString, IConfiguration config = null)

            {

                _ConnectionString = connectionString;



                configuration = config;

            }

            #endregion





            #region SQl Methods

            public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)

            {

                string? conString = ConfigurationExtensions.GetConnectionString(this.configuration, "DefaultConnection");

                using (SqlConnection _db = new SqlConnection(conString))

                {

                    await _db.OpenAsync();

                    return await _db.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.StoredProcedure);

                }

            }



            public async Task<int> ExecuteAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)

            {

                string? conString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");

                using (SqlConnection _db = new SqlConnection(conString))

                {

                    await _db.OpenAsync();

                    return await _db.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);

                }

            }



            public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)

            {

                string? conString = ConfigurationExtensions.GetConnectionString(this.configuration, "DefaultConnection");

                using (SqlConnection con = new SqlConnection(conString))

                {

                    await con.OpenAsync();

                    return await con.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);

                }

            }
        public async Task<SqlMapper.GridReader> QueryMultipleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            string? conString = ConfigurationExtensions.GetConnectionString(this.configuration, "DefaultConnection");

            using (SqlConnection con = new SqlConnection(conString))
            {
                await con.OpenAsync();
                return await con.QueryMultipleAsync(sql,param, commandType: CommandType.StoredProcedure);
            }
               
        }

        public async Task CloseConnAsync()
        {
            string? conString = ConfigurationExtensions.GetConnectionString(this.configuration, "DefaultConnection");
            using (SqlConnection con = new SqlConnection(conString))
            {
                if (Conn.State == ConnectionState.Open)
                    await Conn.CloseAsync();
            }
                
        }


        #endregion
    }
}

