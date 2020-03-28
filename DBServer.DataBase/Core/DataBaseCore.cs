using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DBServer.DataBase.Core
{
    public class DataBaseCore<T>
    {
        string ConnectionString { get; set; }

        public DataBaseCore<T> SetConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryAsync(string SQL, object param = null, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlConnection DB = new SqlConnection(ConnectionString))
                {
                    await DB.OpenAsync().ConfigureAwait(true);
                    var queryResult = await DB.QueryAsync<T>(sql: SQL, param: param, commandType: type).ConfigureAwait(true);
                    return queryResult.AsList<T>();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="param"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<T> QueryFirstOrDefaultAsync(string SQL, object param = null, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlConnection DB = new SqlConnection(ConnectionString))
                {
                    await DB.OpenAsync().ConfigureAwait(true);
                    return await DB.QueryFirstOrDefaultAsync<T>(sql: SQL, param: param, commandType: type).ConfigureAwait(true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="param"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<T> QuerySingleOrDefaultAsync(string SQL, object param = null, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlConnection DB = new SqlConnection())
                {
                    await DB.OpenAsync().ConfigureAwait(true);
                    return await DB.QuerySingleOrDefaultAsync<T>(sql: SQL, param: param, commandType: type).ConfigureAwait(true);
                }   
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="param"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarAsync(string SQL, object param = null, CommandType type = CommandType.Text)
        {
            try
            {
                using (SqlConnection DB = new SqlConnection(ConnectionString))
                {
                    await DB.OpenAsync().ConfigureAwait(true);
                    return await DB.ExecuteScalarAsync<T>(sql: SQL, param: param, commandType: type).ConfigureAwait(true);
                }   
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public sealed class Repository<T>
    {
        private static DataBaseCore<T> instance;

        public static DataBaseCore<T> Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataBaseCore<T>();

                return instance;
            }
        }
    }

    public sealed class SqlResutSet
    {
        public int ID { get; set; }
        public string Message { get; set; }
    }
}
