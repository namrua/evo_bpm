using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Resource.Command;
using CleanEvoBPM.Application.CQRS.Resource.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Resource;
using Dapper;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class ResourceService : IResourceService
    {
        private readonly IDatabaseConnectionFactory _database;
        public ResourceService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<bool> CreateResource(CreateResourceCommand command)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            try
            {
                var result = new GenericResponse();
                var checkExistResourceQuery = db.Query("Resource")
                    .Select("Resource.Id",
                    "Resource.Name",
                    "Resource.Id",
                    "Resource.FromDate",
                    "Resource.ToDate",
                    "Resource.Percentage",
                    "Resource.ContactNumber",
                    "Resource.Email",
                    "Resource.Note")
                    .Where("Resource.Email", command.Email);
                var checkExistResourceResult = await checkExistResourceQuery.GetAsync<ResourceResponseModel>();
                if (checkExistResourceResult.Any())
                {
                    //check whether resource exist or not
                    var checkExistResourceInProjectQuery = db.Query("Resource")
                   .Join("ProjectResource", "Resource.Id", "ProjectResource.ResourceId")
                   .Select("Resource.Id")
                   .Where("ProjectResource.ResourceId", checkExistResourceResult.FirstOrDefault().Id)
                   .Where("ProjectResource.ProjectId", command.Id);
                    var checkExistResourceInProjectResult = await checkExistResourceInProjectQuery.GetAsync<ResourceResponseModel>();
                    if (!checkExistResourceInProjectResult.Any())
                    {
                        //assign resource into project
                        var existResource = checkExistResourceResult.FirstOrDefault();
                        var data = new
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = command.Id,
                            ResourceId = existResource.Id,
                            FromDate = command.FromDate,
                            ToDate = command.ToDate,
                            Percentage = command.Percentage,
                            RoleId = command.RoleId,
                            UpdatedDate = DateTime.Now,
                            CreatedDate = DateTime.Now
                        };
                        var effectedRow = await db.Query("ProjectResource").InsertAsync(data);
                        return effectedRow > 0;
                    }
                    return false;
                }
                else
                {
                    //create a new resource and assign
                    var dataResource = new
                    {
                        Id = Guid.NewGuid(),
                        Name = command.Name,
                        Role = command.RoleId,
                        FromDate = command.FromDate,
                        ToDate = command.ToDate,
                        Percentage = command.Percentage,
                        ContactNumber = command.ContactNumber,
                        Email = command.Email,
                        Note = command.Note,
                        IsActived = command.IsActived,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    var effectedResourceRow = await db.Query("Resource").InsertAsync(dataResource);
                    if (effectedResourceRow > 0)
                    {
                        var dataProjectResource = new
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = command.Id,
                            ResourceId = dataResource.Id,
                            FromDate = dataResource.FromDate,
                            ToDate = dataResource.ToDate,
                            Percentage = dataResource.Percentage,
                            RoleId = dataResource.Role,
                            UpdatedDate = DateTime.Now,
                            CreatedDate = DateTime.Now
                        };
                        var effectedPRRow = await db.Query("ProjectResource").InsertAsync(dataProjectResource);
                        return effectedPRRow > 0;
                    }
                    return false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ResourceResponseModel>> Fetch(FetchResourceQuery query)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            try
            {
                var result = db.Query("Resource")
                    .Join("ProjectResource", "Resource.Id", "ProjectResource.ResourceId")
                    .Join("Role", "Role.Id", "ProjectResource.RoleId")
                    .Select(
                    "ProjectResource.Id as Id",
                    "Resource.Id as ResourceId",
                    "Resource.Name",
                    "Role.Id as RoleId",
                    "Role.Name as Role",
                    "ProjectResource.FromDate",
                    "ProjectResource.ToDate",
                    "ProjectResource.Percentage",
                    "Resource.ContactNumber",
                    "Resource.Email",
                    "Resource.Note")
                    .Where("ProjectResource.ProjectId", query.id)
                    .OrderByDesc("ProjectResource.UpdatedDate");

                return await result.GetAsync<ResourceResponseModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //.WhereTime("ProjectResource.ToDate",">", command.ToDate)
        public async Task<bool> Update(UpdateResourceCommand command)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            try
            {
                //var checkResourceAvailable = await db.Query("ProjectResource")
                //    .Select("Resource.Id")
                //    .Where(x => x.
                //     Where(q => q.WhereTime("ProjectResource.FromDate", "<", command.ToDate).Where(("ProjectResource.ToDate", ">", command.ToDate)))
                //    .OrWhere(s => s.WhereTime("ProjectResource.FromDate", "<", command.FromDate).Where(("ProjectResource.ToDate", ">", command.FromDate))))
                //    .Where("ProjectResource.ResourceId", command.ResourceId)
                //    .FirstOrDefaultAsync<ResourceResponseModel>();
                var resourceParam = new
                {
                    Id = command.ResourceId,
                    Name = command.Name,
                    Email = command.Email,
                    Note = command.Note,
                    UpdatedDate = DateTime.Now,
                    ContactNumber = command.ContactNumber
                };
                var projectResource = new
                {
                    Id = command.Id,
                    ProjectId = command.ProjectId,
                    ResourceId = command.ResourceId,
                    RoleId = command.RoleId,
                    Percentage = command.Percentage,
                    FromDate = command.FromDate,
                    ToDate = command.ToDate
                };
                var effectedRowResource = await conn.ExecuteAsync("Update Resource SET Name = @Name, ContactNumber = @ContactNumber," +
                    "Email = @Email, Note = @Note Where Id = @Id", resourceParam);
                if (effectedRowResource > 0)
                {
                    var effectedRowProjectResource = await conn.ExecuteAsync("Update ProjectResource SET RoleId = @RoleId, Percentage = @Percentage," +
                        "FromDate = @FromDate, ToDate = @ToDate Where Id = @Id", projectResource);
                    return effectedRowResource > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResourceResponseModel> GetResourceDetails(GetResourceDetailQuery query)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            try
            {
                var result = await db.Query("Resource")
                   .Join("ProjectResource", "Resource.Id", "ProjectResource.ResourceId")
                   .Join("Role", "Role.Id", "ProjectResource.RoleId")
                   .Select("Resource.Id",
                   "Resource.Name",
                   "Role.Name as Role",
                   "Role.Id as RoleId",
                   "ProjectResource.FromDate",
                   "ProjectResource.ToDate",
                   "ProjectResource.Percentage",
                   "Resource.ContactNumber",
                   "Resource.Email",
                   "Resource.Note")
                   .Where("ProjectResource.Id", query.Id)
                   //.Where("ProjectResource.ProjectId", Guid.Parse(query.ProjectId))
                   //.Where("Resource.Id", Guid.Parse(query.ResourceId))
                   .FirstOrDefaultAsync<ResourceResponseModel>();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteResourceCommand command)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            try
            {
                var param = new
                {
                    Id = command.Id
                };
                var effectedRow = await conn.ExecuteAsync("Delete From ProjectResource Where Id = @Id", param);
                return effectedRow > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
