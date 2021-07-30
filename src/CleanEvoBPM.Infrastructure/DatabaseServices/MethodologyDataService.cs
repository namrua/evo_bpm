using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Methodology;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SqlKata.Compilers;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Domain;
using CleanEvoBPM.Application.CQRS.Methodology.Event;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class MethodologyDataService : IMethodologyDataService
    {
        private readonly IDatabaseConnectionFactory _database;
        public MethodologyDataService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<GenericResponse> CreateMethodology(CreateMethodologyCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var data = new CreateMethodologyCommand
            {
                Id = Guid.NewGuid(),
                MethodologyName = request.MethodologyName,
                Description = request.Description,
                RecordStatus = request.RecordStatus,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = request.CreatedBy
            };
            

            var affectedRecords = await db.Query("Methodology").InsertAsync(data);

            return new GenericResponse
            {
                Code = 201,
                Success = (affectedRecords > 0)
            };
        }

        public async Task<bool> DeleteMethodology(Guid methodologyId)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var affectedRecords = await db.Query("Methodology").Where("Id", "=", methodologyId).DeleteAsync();
            
            return affectedRecords > 0;
        }

        public async Task<IEnumerable<MethodologyResponseModel>> FetchMethodology(GetMethodologyQuery request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var query = db.Query("Methodology");

            if (request.Id != null)
            {
                query = query.Where("Id", "=", request.Id);
            }
            else if (request.MethodologyName != null)
            {
                query = query.Where("MethodologyName", "=", request.MethodologyName);
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
                        .WhereContains("MethodologyName", keyword)
                        .OrWhereContains("Description", keyword);
                }
            }

            var result = await query.GetAsync<MethodologyResponseModel>();

            return result;
        }

        public async Task<bool> IsMethodologyExisted(string methodologyName)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var result = await db.Query("Methodology").Where("MethodologyName", "=", methodologyName).FirstOrDefaultAsync<MethodologyResponseModel>();

            if (result != null)
                return true;

            return false;
        }

        public async Task<bool> UpdateMethodology(UpdateMethodologyCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var data = new UpdateMethodologyCommand
            {
                Id = request.Id,
                MethodologyName = request.MethodologyName,
                RecordStatus = request.RecordStatus,
                Description = request.Description,
                UpdatedDate = DateTime.UtcNow,
                UpdatedBy = request.UpdatedBy
            };

            var affectedRecords = await db.Query("Methodology").Where("Id", request.Id).UpdateAsync(data);

            return affectedRecords > 0;
        }
    }
}
