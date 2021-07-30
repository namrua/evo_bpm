using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Role;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class RoleService : IRoleService
    {
        private readonly IDatabaseConnectionFactory _database;
        public RoleService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }

        public async Task<IEnumerable<RoleResponseModel>> Fetch()
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            try
            {
                var result = db.Query("Role")
                    .Select("Role.Id",
                    "Role.Name",
                    "Role.Description",
                    "Role.CreatedDate",
                    "Role.UpdatedDate",
                    "Role.IsActived",
                    "Role.CreatedBy",
                    "Role.UpdatedBy",
                    "Role.DeleteFlag")
                    .OrderByDesc("Role.UpdatedDate");
                return await result.GetAsync<RoleResponseModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
