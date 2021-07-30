using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ServiceType;
using System;
using System.Collections.Generic;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Domain;
using CleanEvoBPM.Application.CQRS.ServiceType.Event;
using CleanEvoBPM.Application.CQRS.Status.Event;
namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class ServiceTypeDataService : IServiceTypeDataService
    {
        private readonly IDatabaseConnectionFactory _database;
        public ServiceTypeDataService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<GenericResponse> CreateServiceType(CreateServiceTypeCommand request)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var data = new CreateServiceTypeCommand
                {
                    Id = Guid.NewGuid(),
                    ServiceTypeName = request.ServiceTypeName,
                    RecordStatus = request.RecordStatus,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = request.CreatedBy
                };

                var affectedRecords = await db.Query("ServiceType").InsertAsync(data);

                return new GenericResponse
                {
                    Code = 201,
                    Success = (affectedRecords > 0)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteServiceType(Guid serviceTypeId)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var affectedRecords = await db.Query("ServiceType").Where("Id", "=", serviceTypeId).DeleteAsync();

                return affectedRecords > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ServiceTypeResponseModel>> FetchServiceType(GetServiceTypeQuery request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var query = db.Query("ServiceType");

            if (request.Id != null)
            {
                query = query.Where("Id", "=", request.Id);
            }
            else if (!string.IsNullOrWhiteSpace(request.ServiceTypeName))
            {
                query = query.Where("ServiceTypeName", "=", request.ServiceTypeName);
            }
            else
            {
                if (request.RecordStatus.HasValue)
                {
                    int retInt = Convert.ToInt32(request.RecordStatus);
                    query = query.Where("RecordStatus", "=", retInt);
                }

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    string keyword = request.Search.Trim();

                    query = query
                        .WhereContains("ServiceTypeName", keyword)
                        .OrWhereContains("Description", keyword);
                }
            }

            var result = await query.GetAsync<ServiceTypeResponseModel>();

            return result;
        }

        public async Task<bool> IsServiceTypeExisted(string serviceTypeName)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var result = await db.Query("ServiceType").Where("ServiceTypeName", "=", serviceTypeName).FirstOrDefaultAsync<ServiceTypeResponseModel>();

            if (result != null)
                return true;

            return false;
        }

        public async Task<bool> UpdateServiceType(UpdateServiceTypeCommand request)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var data = new UpdateServiceTypeCommand() { Id = request.Id, ServiceTypeName = request.ServiceTypeName,
                        RecordStatus = request.RecordStatus, UpdatedDate = DateTime.UtcNow, UpdatedBy = request.UpdatedBy };

                var affectedRecords = await db.Query("ServiceType").Where("Id", request.Id).UpdateAsync(data);

                return affectedRecords > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
