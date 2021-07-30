using System;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Status.Command;
using CleanEvoBPM.Application.CQRS.Status.Event;
using CleanEvoBPM.Application.CQRS.Status.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Status;
using CleanEvoBPM.Domain;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class StatusDataService : IStatusDataService
    {
        private readonly IDatabaseConnectionFactory _database;
        public StatusDataService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<GenericListResponse<StatusResponseModel>> FetchStatus()
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = db.Query("Status")
                               .Select(
                                   "Id",
                                   "Name",
                                   "Active",
                                   "UpdatedUser",
                                   "UpdatedDate",
                                   "CreatedDate"
                               ).OrderByDesc("Status.UpdatedDate");                             
                return new GenericListResponse<StatusResponseModel>
                {
                    Code = 200,
                    Content = await result.GetAsync<StatusResponseModel>(),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GenericResponse<StatusResponseModel>> GetStatusDetails(GetStatusDetailsQuery query)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var result = await db.Query("Status")
                                    .Where("Id", query.Id)
                                    .FirstOrDefaultAsync<StatusResponseModel>();
                return new GenericResponse<StatusResponseModel>()
                {
                    Content = result
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GenericResponse> CreateStatus(CreateStatusCommand request)
        {            
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var data = new CreateStatusCommand
            {
                Id          = Guid.NewGuid(),
                Name        = request.Name,
                Active      = request.Active,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = request.CreatedBy
            };

            var affectedRecords = await db.Query("Status").InsertAsync(data);

            return new GenericResponse
            {
                Code = 201,
                Success = (affectedRecords > 0)
            };
        }

        public async Task<bool> UpdateStatus(UpdateStatusCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            //Add to mssql evo DB
            var data = new UpdateStatusCommand() { Id = request.Id, Name = request.Name, 
                    Active = request.Active, UpdatedDate = DateTime.UtcNow, UpdatedBy = request.UpdatedBy };
            var affectedRecords = await db.Query("Status").Where("Id", request.Id).UpdateAsync(data);

            return affectedRecords > 0;
        }

        public async Task<bool> DeleteStatus(Guid id)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var affectedRecords = await db.Query(TableName.Status).Where("Id", "=", id).DeleteAsync();

                return affectedRecords > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}