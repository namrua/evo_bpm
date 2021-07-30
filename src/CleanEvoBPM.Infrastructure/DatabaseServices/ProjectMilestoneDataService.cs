using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectMilestone;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class ProjectMilestoneDataService : IProjectMilestoneDataService
    {

        private readonly IDatabaseConnectionFactory _database;
        public ProjectMilestoneDataService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<bool> CreateProjectMilestones(IEnumerable<ProjectMilestoneModel> milestones, Guid projectId)
        {
            try
            {
                if (milestones == null || !milestones.Any())
                {
                    return true;
                }

                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                milestones = milestones.Select(p =>
                {
                    p.CreatedDate = DateTime.UtcNow;
                    p.UpdatedDate = DateTime.UtcNow;
                    p.Id = Guid.NewGuid();
                    p.ProjectId = projectId;
                    return p;
                }).ToList();

                var affectedRecords = 0;
                foreach(var item in milestones)
                {
                    affectedRecords = await db.Query("ProjectMilestone").InsertAsync(item);
                    if (affectedRecords <= 0)
                        return false;
                }
                return affectedRecords > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteProjectMilestones(Guid projectId)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var affectedRecords = await db.Query("ProjectMilestone").Where("ProjectId", "=", projectId).DeleteAsync();
            return affectedRecords > 0;
        }

        public async Task<IEnumerable<ProjectMilestoneModel>> GetProjectMilestones(Guid projectId)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var result = db.Query("ProjectMilestone")
               .Select(
               "Id",
               "Date",
               "Description",
               "ProjectId",
               "UpdatedUser",
               "UpdatedDate",
               "CreatedDate"
               ).Where("ProjectId", "=", projectId);
            result = result.OrderByDesc("ProjectMilestone.UpdatedDate");
            return await result.GetAsync<ProjectMilestoneModel>();
        }   
    }
}
