using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectLLBP;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class ProjectLLBPDataService : IProjectLLBPDataService
    {
        private readonly IDatabaseConnectionFactory _database;

        public ProjectLLBPDataService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<bool> CreateProjectLLBP(IEnumerable<ProjectLLBPModel> request, Guid projectId)
        {
            try
            {
                if (request == null || !request.Any())
                {
                    return true;
                }
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var listProjectLLBP = new List<object[]>();
                GetDataToInsert(request, listProjectLLBP, projectId);
                var colsB = new[] 
                    { "Id",
                    "ImpactArea",
                    "ProblemSuccessDescription",
                    "ProblemCategory",
                    "LLBPDescription",
                    "ProjectId"
                    };
                var pbm = new Query("ProjectLLBP").AsInsert(colsB, listProjectLLBP);
                var recordPBM = db.Execute(pbm);
                return recordPBM == listProjectLLBP.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateProjectLLBP(IEnumerable<ProjectLLBPModel> request, Guid projectId)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                await db.Query("ProjectLLBP").Where("ProjectId", "=", projectId).DeleteAsync();
                var listProjectLLBP = new List<object[]>();
                GetDataToInsert(request, listProjectLLBP, projectId);
                var colsB = new[]
                    { "Id",
                    "ImpactArea",
                    "ProblemSuccessDescription",
                    "ProblemCategory",
                    "LLBPDescription",
                    "ProjectId"
                    };
                var pbm = new Query("ProjectLLBP").AsInsert(colsB, listProjectLLBP);
                var recordPBM = db.Execute(pbm);
                return recordPBM == listProjectLLBP.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteProjectLLBP(Guid projectId)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            var result = await db.Query("ProjectLLBP").Where("ProjectId", "=", projectId).DeleteAsync();
            return result > 0;
        }

        public async Task<IEnumerable<ProjectLLBPResponseModel>> FetchProjectLLBP(Guid projectId)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                string sql = string.Empty;

                sql = "SELECT Id, " +
                    "ImpactArea, " +
                    "ProblemSuccessDescription, " +
                    "ProblemCategory, " +
                    "LLBPDescription, " +
                    "ProjectId FROM ProjectLLBP WHERE ProjectId= " + "'" + projectId + "'";

                var result = await db.SelectAsync<ProjectLLBPResponseModel>(sql, null);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void GetDataToInsert(IEnumerable<ProjectLLBPModel> projectLLBPs, List<object[]> objs, Guid id)
        {
            foreach (var item in projectLLBPs)
            {
                var obj = new object[]
                {
                    Guid.NewGuid(),
                    item.ImpactArea,
                    item.ProblemSuccessDescription,
                    item.ProblemCategory,
                    item.LLBPDescription,
                    id
                };
                objs.Add(obj);
            }
        }
    }
}
