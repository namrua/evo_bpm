using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.CQRS.Project.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using Dapper;
using SqlKata.Compilers;
using SqlKata.Execution;
using CleanEvoBPM.Application.Models.ProjectBusinessDomain;
using CleanEvoBPM.Application.Models.ProjectMethodology;
using CleanEvoBPM.Application.Models.ProjectTechnology;
using System.Linq;
using SqlKata;
using CleanEvoBPM.Application.CQRS.Project.Event;
using CleanEvoBPM.Domain;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class ProjectDataServices : IProjectDataService
    {
        private readonly IDatabaseConnectionFactory _database;

        public ProjectDataServices(IDatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<bool> CreateProject(CreateProjectCommand request, Guid clientId, Guid projectId)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var data = new 
                {
                    Id               = projectId,   
                    ClientId         = clientId,
                    ProjectName      = request.ProjectName,          
                    ProjectCode      = request.ProjectCode,
                    ProjectManagerId = request.ProjectManagerId,
                    ProjectTypeId    = request.ProjectTypeId,
                    ServiceTypeId    = request.ServiceTypeId,
                    StatusId         = request.StatusId,
                    DeliveryODCId      = request.DeliveryODCId,
                    DeliveryLocationId = request.DeliveryLocationId,
                    StartDate        = request.StartDate,
                    LastUpdated      = DateTime.UtcNow,                    
                    CreatedDate      = DateTime.UtcNow,
                    CreatedBy        = request.CreatedBy,
                };

                var affectedPRojectRecords = await db.Query("Project").InsertAsync(data);

                if (affectedPRojectRecords > 0)
                {
                    var listProjectBusinessDomain = new List<object[]>();
                    GetDataToInsert(request.BusinessDomainId, listProjectBusinessDomain, projectId);
                    var colsB = new [] {"ProjectId", "BusinessDomainId" };
                    var pbm = new Query("ProjectBusinessDomain").AsInsert(colsB, listProjectBusinessDomain);
                    var recordPBM = db.Execute(pbm);

                    var listProjectMethodology = new List<object[]>();
                    GetDataToInsert(request.MethodologyId, listProjectMethodology, projectId);
                    var colsM = new[] { "ProjectId", "MethodologyId" };
                    var pm = new Query("ProjectMethodology").AsInsert(colsM, listProjectMethodology);
                    var recordPM = db.Execute(pm);

                    var listProjectTechnology = new List<object[]>();
                    GetDataToInsert(request.TechnologyId, listProjectTechnology, projectId);
                    var colsP = new[] { "ProjectId", "TechnologyId" };
                    var pt = db.Query("ProjectTechnology").AsInsert(colsP, listProjectTechnology);
                    var recordPT = db.Execute(pt);

                    if (recordPBM == listProjectBusinessDomain.Count 
                        && recordPM == listProjectMethodology.Count
                        && recordPT == listProjectTechnology.Count)
                        return true;
                    else return false;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateProject(UpdateProjectCommand request)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var data = new UpdateProjectCommand {
                    Id               = request.Id,
                    ProjectName      = request.ProjectName,
                    ProjectCode      = request.ProjectCode,
                    ProjectManagerId = request.ProjectManagerId,
                    ProjectTypeId    = request.ProjectTypeId,
                    ServiceTypeId    = request.ServiceTypeId,
                    StatusId         = request.StatusId,
                    DeliveryODCId      = request.DeliveryODCId,
                    DeliveryLocationId = request.DeliveryLocationId,
                    StartDate        = request.StartDate,
                    LastUpdated      = DateTime.UtcNow,
                    UpdatedBy        = request.UpdatedBy
                };

                var affectedRecords = await conn.ExecuteAsync(
                    "UPDATE Project SET " +
                    "ProjectName=@ProjectName, " +
                    "ProjectCode=@ProjectCode, " +
                    "ProjectManagerId=@ProjectManagerId, " +
                    "StartDate=@StartDate, " +
                    "ProjectTypeId=@ProjectTypeId, " +
                    "ServiceTypeId=@ServiceTypeId, " +
                    "StatusId=@StatusId, " +
                    "DeliveryODCId=@DeliveryODCId, " +
                    "DeliveryLocationId=@DeliveryLocationId, " +
                    "LastUpdated=@LastUpdated, " +
                    "UpdatedBy=@UpdatedBy " +
                    "WHERE Id = @Id",
                    data);

                if (affectedRecords > 0)
                {
                    await db.Query("ProjectBusinessDomain").Where("ProjectId", "=", request.Id).DeleteAsync();
                    var listProjectBusinessDomain = new List<object[]>();
                    GetDataToInsert(request.BusinessDomainId, listProjectBusinessDomain, request.Id);
                    var colsB = new[] { "ProjectId", "BusinessDomainId" };
                    var pbm = new Query("ProjectBusinessDomain").AsInsert(colsB, listProjectBusinessDomain);
                    var recordPBM = db.Execute(pbm);

                    await db.Query("ProjectMethodology").Where("ProjectId", "=", request.Id).DeleteAsync();
                    var listProjectMethodology = new List<object[]>();
                    GetDataToInsert(request.MethodologyId, listProjectMethodology, request.Id);
                    var colsM = new[] { "ProjectId", "MethodologyId" };
                    var pm = new Query("ProjectMethodology").AsInsert(colsM, listProjectMethodology);
                    var recordPM = db.Execute(pm);

                    await db.Query("ProjectTechnology").Where("ProjectId", "=", request.Id).DeleteAsync();
                    var listProjectTechnology = new List<object[]>();
                    GetDataToInsert(request.TechnologyId, listProjectTechnology, request.Id);
                    var colsP = new[] { "ProjectId", "TechnologyId" };
                    var pt = db.Query("ProjectTechnology").AsInsert(colsP, listProjectTechnology);
                    var recordPT = db.Execute(pt);

                    if (recordPBM == listProjectBusinessDomain.Count
                            && recordPM == listProjectMethodology.Count
                            && recordPT == listProjectTechnology.Count)
                        return true;
                    else return false;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteProject(Guid projectId)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var affectedRecords = await db.Query("Project").Where("Id", "=", projectId).DeleteAsync();

                return affectedRecords > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public async Task<IEnumerable<ProjectResponseModel>> FetchProject(int pageNumber,int pageSize)
        public async Task<IEnumerable<ProjectResponseModel>> FetchProject(FetchProjectQuery request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            try
            {
                var result = db.Query("Project")
                    .LeftJoin("Client", "Client.Id", "Project.ClientId")
                    .LeftJoin("ProjectType", "ProjectType.Id", "Project.ProjectTypeId")
                    .LeftJoin("ServiceType", "ServiceType.Id", "Project.ServiceTypeId")
                    .LeftJoin("Status", "Status.Id", "Project.StatusId")
                    .Select(
                        "Project.Id",
                        "Client.ClientName",
                        "ProjectType.ProjectTypeName",
                        "ServiceType.ServiceTypeName",
                        "Status.Name",
                        "Project.ProjectName",
                        "Project.LastUpdated",
                        "ClientId",
                        "ProjectName",
                        "ProjectCode",
                        "ProjectManagerId",
                        "ProjectTypeId",
                        "ServiceTypeId",
                        "StatusId",
                        "DeliveryLocationId",
                        "DeliveryODCId",
                        "StartDate",
                        "Project.CreatedDate"
                    ).OrderByDesc("Project.LastUpdated");

                if (!string.IsNullOrEmpty(request.Search) && request.Search.Trim() != "")
                {
                    result = result.WhereContains("ProjectName", request.Search.Trim())
                        .OrWhereContains("ProjectCode", request.Search.Trim())
                        .OrWhereContains("ProjectTypeName", request.Search.Trim())
                        .OrWhereContains("ServiceTypeName", request.Search.Trim())
                        .OrWhereContains("ClientName", request.Search.Trim());
                }
                return await result.GetAsync<ProjectResponseModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProjectExportModel>> Export(ExportProject exportParams)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = db.Query("Project")
                    .Join("Client", "Client.Id", "Project.ClientId")
                    .Join("ProjectType", "ProjectType.Id", "Project.ProjectTypeId")
                    .Join("Methodology", "Methodology.Id", "Project.MethodologyId")
                    .Join("ServiceType", "ServiceType.Id", "Project.ServiceTypeId")
                    .Join("Status", "Status.Id", "Project.StatusId")
                    .Select(
                        "Project.Id",
                        "Client.ClientName",
                        "ProjectType.ProjectTypeName",
                        "Methodology.MethodologyName",
                        "ServiceType.ServiceTypeName",
                        "Status.Name",
                        "Project.ProjectName",
                        "Project.LastUpdated",
                        "Project.CreatedDate",
                        "Project.ProjectCode"
                    );
                if (!string.IsNullOrEmpty(exportParams.Search) && exportParams.Search.Trim() != "")
                {
                    result = result.WhereContains("ProjectName", exportParams.Search.Trim())
                        .OrWhereContains("ProjectCode", exportParams.Search.Trim())
                        .OrWhereContains("ProjectTypeName", exportParams.Search.Trim())
                        .OrWhereContains("MethodologyName", exportParams.Search.Trim())
                        .OrWhereContains("ServiceTypeName", exportParams.Search.Trim())
                        .OrWhereContains("ClientName", exportParams.Search.Trim());
                };

                return await result.GetAsync<ProjectExportModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProjectResponseModel> GetById(Guid id)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            var result = await db.Query("Project")
                .Select(
                "Id",
                "ClientId",
                "ProjectName",
                "ProjectCode",
                "ProjectManagerId",
                "ProjectTypeId",
                "ServiceTypeId",
                "StatusId",
                "DeliveryODCId",
                "DeliveryLocationId",
                "StartDate",
                "LastUpdated"
                )
                .Where("Id", "=", id)
                .FirstOrDefaultAsync<ProjectResponseModel>();
            return result;
        }

        public async Task<IEnumerable<Guid>> FetchListProjectBusinessDomain(Guid id)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var result = await db.Query("ProjectBusinessDomain").Select("BusinessDomainId").Where("ProjectId", "=", id).GetAsync<Guid>();
            return result;
        }

        public async Task<IEnumerable<Guid>> FetchListProjectMethodology(Guid id)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var result = await db.Query("ProjectMethodology").Select("MethodologyId").Where("ProjectId", "=", id).GetAsync<Guid>();
            return result;
        }

        public async Task<IEnumerable<Guid>> FetchListProjectTechnology(Guid id)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var result = await db.Query("ProjectTechnology").Select("TechnologyId").Where("ProjectId", "=", id).GetAsync<Guid>();
            return result;
        }

        public async Task<IEnumerable<ProjectResponseModel>> GetProjectsByManagerId(Guid id)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var query = db.Query("Project")
                .Select(
                    "Id",
                    "ClientId",
                    "ProjectName",
                    "StartDate",
                    "ProjectCode",
                    "ProjectManagerId",
                    "StatusId"
                )
                .Where("ProjectManagerId", "=", id);

            return await query.GetAsync<ProjectResponseModel>();
        }

        public async Task<IEnumerable<ProjectCountByStatusModel>> GetProjectCountByStatus()
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var query = db.Query("Project")
                .Join("Status", "Project.StatusId", "Status.Id")
                .Select("Status.Id as StatusId", "Status.Name as Status")
                .SelectRaw("count(1) as ProjectCount")
                .GroupBy("Status.Id", "Status.Name");

            return await query.GetAsync<ProjectCountByStatusModel>();
        }

        private void GetDataToInsert(List<Guid> ids, List<object[]> objs, Guid id)
        {
            foreach (var item in ids)
            {
                var obj = new object[]
                {
                            id,item
                };
                objs.Add(obj);
            }
        }
    }
}