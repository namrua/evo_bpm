using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using SqlKata.Compilers;
using SqlKata.Execution;
using CleanEvoBPM.Application.CQRS.Technology.Command;
using CleanEvoBPM.Application.Models.Technology;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Domain;
using CleanEvoBPM.Application.CQRS.Technology.Event;
using CleanEvoBPM.Application.Common;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class TechnologyDataService : ITechnologyDataService
    {

        private readonly IDatabaseConnectionFactory _database;

        public TechnologyDataService(IDatabaseConnectionFactory database)
        {            
            _database = database;
        }

        public async Task<GenericResponse> Create(CreateTechnologyCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var data = new TechnologyCommit
            {
                Id                    = Guid.NewGuid(),
                TechnologyName        = request.TechnologyName,
                TechnologyDescription = request.TechnologyDescription,
                TechnologyActive      = request.TechnologyActive,
                CreatedDate           = DateTime.UtcNow,
                UpdatedDate           = DateTime.UtcNow,
                IsDeleted             = false,
                CreatedBy             = request.CreatedBy,
                
            };

            var affectedRecords = await db.Query("Technology").InsertAsync(data);            

            return new GenericResponse
            {
                Code = 201,
                Success = (affectedRecords > 0),
            };
        }

        public async Task<bool> Delete(Guid technologyId)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var parameters = new TechnologyCommit
            {
                Id = technologyId
            };

            var affectedRecords = await conn.ExecuteAsync("UPDATE Technology SET IsDeleted=1 WHERE ID = @ID",parameters);

            return affectedRecords > 0;
        }

        public async Task<IEnumerable<TechnologyResponseModel>> Fetch(FetchTechnologyQuery query)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

             var result = db.Query("Technology")
                .Select(
                "ID",
                "TechnologyName",
                "TechnologyDescription",
                "TechnologyActive",
                "CreatedDate",
                "UpdatedDate",
                "UpdatedUser"
                );

            result = result.Where(x=> x.WhereFalse("IsDeleted").OrWhereNull("IsDeleted"));

            if(!string.IsNullOrEmpty(query.Search) && query.Search.Trim() !="")
            {
                result= result.Where(x=> x.WhereContains("TechnologyName", query.Search).OrWhereContains("TechnologyDescription", query.Search));
            }

            if(query.Active!=null){
                result = query.Active ==true?  result.WhereTrue("TechnologyActive") : result.WhereFalse("TechnologyActive");
            }

            result=result.OrderByDesc("Technology.UpdatedDate");
            return await result.GetAsync<TechnologyResponseModel>();
        }

        public async Task<GenericResponse> Update(UpdateTechnologyCommand request)
        {
            using var conn = await _database.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());

            var parameters = new TechnologyCommit
            {
                Id = request.Id,
                TechnologyName = request.TechnologyName,
                TechnologyDescription = request.TechnologyDescription,
                TechnologyActive = request.TechnologyActive,
                UpdatedDate = DateTime.UtcNow,
                UpdatedUser = Guid.NewGuid(),
                UpdatedBy = request.UpdatedBy
            };
            var affectedRecords = await conn.ExecuteAsync("UPDATE Technology SET TechnologyName=@TechnologyName, " +
                " TechnologyDescription=@TechnologyDescription, TechnologyActive=@TechnologyActive, UpdatedDate=@UpdatedDate, UpdatedUser=@UpdatedUser, UpdatedBy =@UpdatedBy WHERE ID = @ID",
                parameters);

            return new GenericResponse
            {
                Code = 201,
                Success = (affectedRecords > 0)
            };
        }

        public async Task<TechnologyResponseModel> GetTechnologyDetail(GetTechnologyDetailsQuery query)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                TechnologyResponseModel result = null;
                if(query.Id != null)
                {
                    result = await db.Query("Technology").Where(x=> x.WhereFalse("IsDeleted").OrWhereNull("IsDeleted")).Where("Id", query.Id).FirstOrDefaultAsync<TechnologyResponseModel>();
                }
                else if(!string.IsNullOrEmpty(query.Name))
                {
                    result = await db.Query("Technology").Where(x=> x.WhereFalse("IsDeleted").OrWhereNull("IsDeleted")).Where("TechnologyName", query.Name).FirstOrDefaultAsync<TechnologyResponseModel>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
