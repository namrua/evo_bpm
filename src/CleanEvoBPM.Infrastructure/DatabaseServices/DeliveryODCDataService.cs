using CleanEvoBPM.Application.CQRS.DeliveryODC.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.DeliveryODC;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class DeliveryODCDataService : IDeliveryODCDataService
    {
        private readonly IDatabaseConnectionFactory _database;
        public DeliveryODCDataService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<IEnumerable<DeliveryODCResponseModel>> Fetch(GetDeliveryODCQuery request)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = db.Query("DeliveryODC")
                                   .Select(
                                       "Id",
                                       "Name",
                                       "Description",
                                       "Status",
                                       "LastUpdated",
                                       "CreatedDate",
                                       "UpdatedBy",
                                       "CreatedBy"
                                   ).OrderByDesc("DeliveryODC.LastUpdated");
                if (request.Status != null)
                {
                    result.Where("Status", "=", request.Status);
                }
                return await result.GetAsync<DeliveryODCResponseModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DeliveryODCResponseModel> GetById(Guid id)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = await db.Query("DeliveryODC")
                                   .Select(
                                       "Id",
                                       "Name",
                                       "Description",
                                       "Status",
                                       "LastUpdated",
                                       "CreatedDate",
                                       "UpdatedBy",
                                       "CreatedBy"
                                   ).Where("Id", "=", id).FirstOrDefaultAsync<DeliveryODCResponseModel>();
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
