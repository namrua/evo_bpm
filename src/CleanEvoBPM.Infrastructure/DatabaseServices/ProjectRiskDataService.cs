using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectRisk;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Linq;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class ProjectRiskDataService : IProjectRiskDataService
    {
        private readonly IDatabaseConnectionFactory _database;
        public ProjectRiskDataService(IDatabaseConnectionFactory database)
        {
            _database = database;

        }
        public async Task<bool> CreateManyProjectRisk(IEnumerable<string> risks, Guid projectId)
        {
            try
            {
                if (risks==null || !risks.Any())
                {
                    return true;
                }
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var listProjectRisks = new List<ProjectRiskModel>();
                var risk = new ProjectRiskModel();
                var affectedRecords = 0;
                foreach (var item in risks)
                {
                    risk.Id = Guid.NewGuid();
                    risk.CreatedDate = DateTime.UtcNow;
                    risk.UpdatedDate = DateTime.UtcNow;
                    risk.Name = item;
                    risk.ProjectId = projectId;
                    listProjectRisks.Add(risk);
                    affectedRecords = await db.Query("ProjectRisk").InsertAsync(risk);
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

        public async Task<bool> DeleteProjectRisk(Guid projectId)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            var affectedRecords = await db.Query("ProjectRisk").Where("ProjectId","=",projectId).DeleteAsync();
            return affectedRecords > 0;
        }

        public async Task<IEnumerable<ProjectRiskModel>> GetProjectRisks(Guid projectId)
        {
             using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var result = db.Query("ProjectRisk")
                        .Select(
                            "Id",
                            "Name"
                        ).Where("ProjectId", "=", projectId);
            result = result.OrderByDesc("ProjectRisk.UpdatedDate");
            return await result.GetAsync<ProjectRiskModel>();
        }
        
    }
}