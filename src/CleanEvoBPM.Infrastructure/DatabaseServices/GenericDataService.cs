using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class GenericDataService<T> : IGenericDataService<T> where T : class
    {
        private readonly IDatabaseConnectionFactory _database;
        public GenericDataService(IDatabaseConnectionFactory database)
        {
            _database = database;

        }

        // public async Task<bool> IsNToNRelationship(string tableName, string colunmName,Guid statusId)
        // {
        //     try
        //     {
        //         using var conn = await _database.CreateConnectionAsync();
        //         var db = new QueryFactory(conn, new SqlServerCompiler());
        //         var result = await db.Query(tableName).Select(colunmName).GetAsync<Guid>();
        //         if ()
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        //     return true;
        // }

        public async Task<IEnumerable<T>> GetAll(string tableName)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = await db.Query(tableName).GetAsync<T>();

                return result;
            }
            catch (Exception ex)
            {  
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetListIgnoreByID(string tableName, string ignoreByID)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var result = await db.Query(tableName).Where("Id", "!=", ignoreByID).GetAsync<T>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsUniqueName(string tableName,string columnName,string valueName, string id)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var query =  db.Query(tableName).Where(columnName,"=", valueName);
                if (!string.IsNullOrEmpty(id))
                {
                    query = query.Where("Id", "!=", id);
                }

                var result = await query.FirstOrDefaultAsync<T>();

                return result==null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}