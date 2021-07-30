using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Event;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.BusinessDomain;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class BusinessDomainDataService : IBusinessDomainDataService
    {
        private readonly IDatabaseConnectionFactory _database;
        public BusinessDomainDataService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<GenericResponse> CreatBusinessDomain(CreateBusinessDomainCommand request)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var data = new CreateBusinessDomainCommand
                {
                    Id = Guid.NewGuid(),
                    BusinessDomainName = request.BusinessDomainName,
                    Description = request.Description,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = request.CreatedBy,
                    Status = request.Status
                };

                var affectedRecords = await db.Query("BusinessDomain").InsertAsync(data);

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

        public async Task<bool> DeleteBusinessDomain(DeleteBusinessDomainCommand requets)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var affectedRecord = db.Query("BusinessDomain").Where("Id", "=", requets.Id).Delete();

                return affectedRecord > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<BusinessDomainResponseModel>> FetchBusinessDomain(FetchBusinessDomainQuery query)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = db.Query("BusinessDomain")
                               .Select(
                                   "Id",
                                   "BusinessDomainName",
                                   "Description",
                                   "CreatedDate",
                                   "UpdatedDate",
                                   "Status"
                               ).OrderByDesc("BusinessDomain.UpdatedDate");

                if (!string.IsNullOrEmpty(query.Search) && query.Search.Trim() != "")
                {
                    result = result.WhereContains("BusinessDomainName", query.Search.Trim())
                                    .OrWhereContains("Description", query.Search.Trim());
                }
                if (query.Status != null)
                {
                    result.Where("Status", "=", true);
                }
                return await result.GetAsync<BusinessDomainResponseModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BusinessDomainResponseModel> GetBusinessDomainDetail(GetBusinessDomainDetailsQuery request)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = new BusinessDomainResponseModel();
                if (request.Id != Guid.Empty)
                {
                    result = await db.Query("BusinessDomain")
                                    .Where("Id", request.Id)
                                    .FirstOrDefaultAsync<BusinessDomainResponseModel>();
                } else if (!string.IsNullOrEmpty(request.Name))
                {
                    result = await db.Query("BusinessDomain")
                                    .Where("BusinessDomainName", request.Name)
                                    .FirstOrDefaultAsync<BusinessDomainResponseModel>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsUniqueName(string businessDomainName)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = await db.Query("BusinessDomain")
                .Where("BusinessDomainName", "=", businessDomainName)
                .FirstOrDefaultAsync();
                if (result == null) return true; return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateBusinessDomain(UpdateBusinessDomainCommand requets)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var data = new UpdateBusinessDomainCommand {
                    Id = requets.Id,
                    BusinessDomainName = requets.BusinessDomainName.Trim(),
                    Description = requets.Description,
                    UpdatedDate = DateTime.UtcNow,
                    Status = requets.Status,
                    UpdatedBy = requets.UpdatedBy
                };

                var affectedRecords = await db.Query("BusinessDomain").Where("Id", requets.Id).UpdateAsync(data);

                //new {
                //    BusinessDomainName = requets.BusinessDomainName.Trim(),
                //    Description = requets.Description,
                //    UpdatedDate = DateTime.UtcNow,
                //    Status = requets.Status
                //});

                return affectedRecords > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}