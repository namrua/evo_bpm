using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectType;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectType.Query;
using CleanEvoBPM.Domain;
using CleanEvoBPM.Application.CQRS.ProjectType.Event;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class ProjectTypeDataService : IProjectTypeDataService
    {
        private readonly IDatabaseConnectionFactory _database;
        public ProjectTypeDataService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }
        
        public async Task<GenericResponse> CreateProjectType(CreateProjectTypeCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var data = new CreateProjectTypeCommand
            {
                Id = Guid.NewGuid(),
                ProjectTypeName = request.ProjectTypeName,
                ProjectTypeDescription = request.ProjectTypeDescription,
                RecordStatus = request.RecordStatus,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = request.CreatedBy
            };

            var affectedRecords = await db.Query("ProjectType").InsertAsync(data);

            return new GenericResponse
            {
                Code = 201,
                Success = (affectedRecords > 0)
            };
        }

        public async Task<bool> DeleteProjectType(Guid projectTypeId)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var affectedRecords = await db.Query("ProjectType").Where("Id", "=", projectTypeId).DeleteAsync();

            return affectedRecords > 0;
        }

        public async Task<IEnumerable<ProjectTypeResponseModel>> FetchProjectType(GetProjectTypeQuery request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var result = db.Query("ProjectType")
                               .Select(
                                   "Id",
                                   "ProjectTypeName",
                                   "ProjectTypeDescription",
                                   "UpdatedDate",
                                   "CreatedDate",
                                   "RecordStatus"
                               );

                result = result.Where(x=> x.WhereFalse("IsDeleted").OrWhereNull("IsDeleted"));

                if(!string.IsNullOrEmpty(request.ProjectTypeName) && request.ProjectTypeName.Trim() !="")
                {
                    result= result.Where(x=> x.WhereContains("ProjectTypeName", request.ProjectTypeName).OrWhereContains("ProjectTypeDescription", request.ProjectTypeName));
                }

                if (request.Id != null)
                {
                    result = result.Where("Id", "=", request.Id);
                }

                if (request.RecordStatus != null)
                {
                    result = result.Where("RecordStatus", "=", request.RecordStatus);
                }

                result =result.OrderByDesc("ProjectType.UpdatedDate");
                return await result.GetAsync<ProjectTypeResponseModel>();
        }

        public async Task<bool> IsProjectTypeExisted(string projectTypeName)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var result = await db.Query("ProjectType").Where("ProjectTypeName", "=", projectTypeName).FirstOrDefaultAsync<ProjectTypeResponseModel>();

            if (result != null)
                return true;

            return false;
        }

        public async Task<ProjectTypeResponseModel> GetProjectTypeDetail(GetProjectTypeDetailsQuery query)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                ProjectTypeResponseModel result = null;
                if(query.Id != null)
                {
                    result = await db.Query("ProjectType").Where(x=> x.WhereFalse("IsDeleted").OrWhereNull("IsDeleted")).Where("Id", query.Id).FirstOrDefaultAsync<ProjectTypeResponseModel>();
                }
                else if(!string.IsNullOrEmpty(query.Name))
                {
                    result = await db.Query("ProjectType").Where(x=> x.WhereFalse("IsDeleted").OrWhereNull("IsDeleted")).Where("ProjectTypeName", query.Name).FirstOrDefaultAsync<ProjectTypeResponseModel>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GenericResponse> UpdateProjectType(UpdateProjectTypeCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var data = new UpdateProjectTypeCommand
            {                
                Id                     = request.Id,
                ProjectTypeName        = request.ProjectTypeName,
                ProjectTypeDescription = request.ProjectTypeDescription,
                RecordStatus           = request.RecordStatus,
                UpdatedDate            = DateTime.UtcNow,
                UpdatedBy              = request.UpdatedBy
            };

            var affectedRecords = await db.Query("ProjectType").Where("Id", request.Id).UpdateAsync(data);

            return new GenericResponse
            {
                Code = 201,
                Success = (affectedRecords > 0)
            };
        }
    }
}
