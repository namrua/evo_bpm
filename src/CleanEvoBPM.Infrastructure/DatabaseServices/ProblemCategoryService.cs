using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Event;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProblemCategory;
using CleanEvoBPM.Application.Models.Technology;
using CleanEvoBPM.Domain;
using Dapper;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class ProblemCategoryService : IProblemCategoryService
    {
        private readonly IDatabaseConnectionFactory _database;
        public ProblemCategoryService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<IEnumerable<ProblemCategoryResponseModel>> Fetch(FetchProblemCategoryQuery query)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = db.Query("ProblemCategory")
                    .Select(
                    "ID",
                    "Name",
                    "Description",
                    "IsActived",
                    "CreatedDate",
                    "UpdatedDate",
                    "UpdatedBy",
                    "CreatedBy",
                    "DeleteFlag"
                    );

                result = result.Where(x => x.WhereFalse("DeleteFlag").OrWhereNull("DeleteFlag"));
                if (!string.IsNullOrEmpty(query.Search) && query.Search.Trim() != "")
                {
                    result = result.Where(x => x.WhereContains("Name", query.Search).OrWhereContains("Description", query.Search));
                }

                if (query.Active != null)
                {
                    result = query.Active == true ? result.WhereTrue("IsActived") : result.WhereFalse("IsActived");
                }

                result = result.OrderByDesc("ProblemCategory.UpdatedDate");
                return await result.GetAsync<ProblemCategoryResponseModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProblemCategoryResponseModel> GetProblemCategoryDetail(GetProblemCategoryDetailQuery query)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                ProblemCategoryResponseModel result = null;
                if (query.Id != null)
                {
                    result = await db.Query("ProblemCategory").Where(x => x.WhereFalse("DeleteFlag").OrWhereNull("DeleteFlag")).Where("Id", query.Id).FirstOrDefaultAsync<ProblemCategoryResponseModel>();
                }
                else if (!string.IsNullOrEmpty(query.Name))
                {
                    result = await db.Query("ProblemCategory").Where(x => x.WhereFalse("DeleteFlag").OrWhereNull("DeleteFlag")).Where("Name", query.Name).FirstOrDefaultAsync<ProblemCategoryResponseModel>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Create(CreateProblemCategoryCommand command)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var data = new CreateProblemCategoryCommand
                {
                    Id = Guid.NewGuid(),
                    Name = command.Name,
                    Description = command.Description,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActived = command.IsActived,
                    CreatedBy = command.CreatedBy,
                    UpdatedBy = command.CreatedBy,
                    DeleteFlag = command.DeleteFlag,
                };

                var affectedRecords = await db.Query("ProblemCategory").InsertAsync(data);

                return affectedRecords > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(UpdateProblemCategoryCommand request)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var data = new UpdateProblemCategoryCommand
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    IsActived = request.IsActived,
                    UpdatedDate = DateTime.UtcNow,
                    UpdatedBy = request.UpdatedBy
                };

                var parameters = new
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    IsActived = request.IsActived,
                    UpdatedDate = DateTime.UtcNow,
                    UpdatedBy = request.UpdatedBy
                };
                var affectedRecords = await conn.ExecuteAsync("UPDATE ProblemCategory SET name=@Name, " +
                    " Description=@Description, IsActived=@IsActived, UpdatedDate=@UpdatedDate, UpdatedBy=@UpdatedBy WHERE ID = @ID",
                    parameters);

                //var affectedRecords2 = await db.Query("ProblemCategory").Where("Id", request.Id).UpdateAsync(data);

                return affectedRecords > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            };

        }

        public async Task<bool> Delete(DeleteProblemCategoryCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var parameters = new
            {
                Id = request.Id
            };

            var affectedRecords = await conn.ExecuteAsync("UPDATE ProblemCategory SET DeleteFlag = 1 WHERE ID = @ID", parameters);

            //var affectedRecords2 = await db.Query("ProblemCategory").Where("Id", "=", request.Id).DeleteAsync();

            return affectedRecords > 0;
        }
    }

}