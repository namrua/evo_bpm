using CleanEvoBPM.Application.CQRS.DeliveryLocation.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.DeliveryLocation;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class DeliveryLocationDataService : IDeliveryLocationDataService
    {
        private readonly IDatabaseConnectionFactory _database;
        public DeliveryLocationDataService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<IEnumerable<DeliveryLocationResponseModel>> Fetch(GetDeliveryLocationQuery request)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = db.Query("DeliveryLocation")
                                   .Select(
                                       "Id",
                                       "Name",
                                       "Description",
                                       "Status",
                                       "LastUpdated",
                                       "CreatedDate",
                                       "UpdatedBy",
                                       "CreatedBy"
                                   ).OrderByDesc("DeliveryLocation.LastUpdated");
                if (request.Status != null)
                {
                    result.Where("Status", "=", request.Status);
                }
                return await result.GetAsync<DeliveryLocationResponseModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DeliveryLocationResponseModel> GetById(Guid id)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = await db.Query("DeliveryLocation")
                                   .Select(
                                       "Id",
                                       "Name",
                                       "Description",
                                       "Status",
                                       "LastUpdated",
                                       "CreatedDate",
                                       "UpdatedBy",
                                       "CreatedBy"
                                   ).Where("Id", "=", id).FirstOrDefaultAsync<DeliveryLocationResponseModel>();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
