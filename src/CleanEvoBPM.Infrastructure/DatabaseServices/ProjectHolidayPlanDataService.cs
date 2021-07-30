using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Command;
using CleanEvoBPM.Application.CQRS.ProjectHolidayPlan.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectHolidayPlan;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class ProjectHolidayPlanDataService : IProjectHolidayPlanDataService
    {
        private readonly IDatabaseConnectionFactory _database;
        private readonly string TableName = "ProjectHolidayPlan";

        public ProjectHolidayPlanDataService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<GenericResponse> Create(CreateProjectHolidayPlanCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            request.Id = Guid.NewGuid();
            request.CreatedDate = DateTime.UtcNow;
            request.UpdatedDate = DateTime.UtcNow;

            var affectedRecords = await db.Query(TableName).InsertAsync(request);

            return new GenericResponse
            {
                Success = (affectedRecords > 0)
            };
        }

        public async Task<GenericResponse> Delete(Guid id)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var affectedRecords = await db.Query(TableName).Where("Id", "=", id).DeleteAsync();

            return new GenericResponse
            {
                Success = (affectedRecords > 0)
            };
        }

        public async Task<IEnumerable<ProjectHolidayPlanResponseModel>> Get(GetProjectHolidayPlanQuery request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var query = db.Query(TableName)
                .LeftJoin("Resource", "Resource.Id", string.Format("{0}.ResourceId", TableName))
                .LeftJoin("Role", "Role.Id", string.Format("{0}.ResourceRoleId", TableName))
                .Select(
                    string.Format("{0}.Id", TableName),
                    string.Format("{0}.ProjectId", TableName),
                    string.Format("{0}.ResourceId", TableName),
                    "Resource.Name as Resource",
                    string.Format("{0}.ResourceRoleId", TableName),
                    "Role.Name as ResourceRole",
                    string.Format("{0}.FromDate", TableName),
                    string.Format("{0}.ToDate", TableName),
                    string.Format("{0}.Note", TableName),
                    string.Format("{0}.CreatedDate", TableName),
                    string.Format("{0}.UpdatedDate", TableName),
                    string.Format("{0}.CreatedBy", TableName),
                    string.Format("{0}.LastUpdatedBy", TableName)
                );

            if (request.ProjectId != null)
            {
                query = query.Where("ProjectId", "=", request.ProjectId);
            }

            var result = await query.GetAsync<ProjectHolidayPlanResponseModel>();

            return result;
        }

        public async Task<GenericResponse> Update(UpdateProjectHolidayPlanCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            request.UpdatedDate = DateTime.UtcNow;

            var data = new
            {
                request.Id,
                request.ResourceId,
                request.ResourceRoleId,
                request.FromDate,
                request.ToDate,
                request.Note,
                request.UpdatedDate,
                request.LastUpdatedBy
            };

            var affectedRecords = await db.Query(TableName)
                .Where("Id", request.Id)
                .UpdateAsync(data);

            return new GenericResponse
            {
                Success = (affectedRecords > 0)
            };
        }

        public async Task<ProjectHolidayPlanResponseModel> GetById(Guid id)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            var result = await db.Query(TableName)
                .Select(
                    "Id",
                    "ProjectId",
                    "ResourceId",
                    "ResourceRoleId",
                    "FromDate",
                    "ToDate",
                    "Note",
                    "CreatedDate",
                    "UpdatedDate",
                    "CreatedBy",
                    "LastUpdatedBy"
                )
                .Where("Id", "=", id)
                .FirstOrDefaultAsync<ProjectHolidayPlanResponseModel>();

            return result;
        }
    }
}
