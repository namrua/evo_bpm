using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.CQRS.Client.Event;
using CleanEvoBPM.Application.CQRS.Client.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Client;
using CleanEvoBPM.Domain;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace CleanEvoBPM.Infrastructure.DatabaseServices
{
    public class ClientDataService : IClientDataService
    {
        private readonly IDatabaseConnectionFactory _database;
        public ClientDataService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<ClientResponseModel> GetClient(GetClientDetailQuery query)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                ClientResponseModel result = null;
                if(query.Id != null)
                {
                    result = await db.Query("Client").Where(x=> x.WhereFalse("IsDeleted").OrWhereNull("IsDeleted")).Where("Id", query.Id).FirstOrDefaultAsync<ClientResponseModel>();
                }
                else if(!string.IsNullOrEmpty(query.Name))
                {
                    result = await db.Query("Client").Where(x=> x.WhereFalse("IsDeleted").OrWhereNull("IsDeleted")).Where("ClientName", query.Name).FirstOrDefaultAsync<ClientResponseModel>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ClientResponseModel>> GetClients(FetchClientQuery request)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var result = db.Query("Client")
                               .Select(
                                   "Id",
                                   "ClientName",
                                   "ClientDivisionName",
                                   "UpdatedDate",
                                   "CreatedDate"
                               );

                result = result.Where(x=> x.WhereFalse("IsDeleted").OrWhereNull("IsDeleted"));

                if(!string.IsNullOrEmpty(request.Search) && request.Search.Trim() !="")
                {
                    result= result.Where(x=> x.WhereContains("ClientName", request.Search).OrWhereContains("ClientDivisionName", request.Search));
                }

                result=result.OrderByDesc("Client.UpdatedDate");
                return await result.GetAsync<ClientResponseModel>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GenericResponse<ClientResponseModel>> CreateClient(CreateClientCommand request)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                                
                var clientToInsert = new ClientResponseModel                
                {
                    Id = Guid.NewGuid(),
                    ClientName = request.ClientName,
                    ClientDivisionName = request.ClientDivisionName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = request.CreatedBy
                };
                
                var affectedRecords = await db.Query("Client").InsertAsync(clientToInsert);


                return new GenericResponse<ClientResponseModel>
                {
                    Code = 201,
                    Success = (affectedRecords > 0),
                    Content = clientToInsert

                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteClient(DeleteClientCommand requets)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var affectedRecord = db.Query("Client")
                .Where("Id", "=", requets.Id)
                .Delete();

                return affectedRecord > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GenericResponse> UpdateClient(UpdateClientCommand requets)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());

                var data = new UpdateClientCommand
                {
                    Id = requets.Id,
                    ClientName = requets.ClientName,
                    ClientDivisionName = requets.ClientDivisionName,
                    UpdatedDate = DateTime.UtcNow,
                    UpdatedBy = requets.UpdatedBy
                };

                var affectedRecords = await db.Query("Client").Where("Id", requets.Id).UpdateAsync(data);

                return new GenericResponse
                {
                    Code = 201,
                    Success = (affectedRecords > 0)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ClientResponseModel> GetClientByName(string name)
        {
            try
            {
                using var conn = await _database.CreateConnectionAsync();
                var db = new QueryFactory(conn, new SqlServerCompiler());
                var result = await db.Query("Client")
                                    .Where("ClientName","=", name)
                                    .FirstOrDefaultAsync<ClientResponseModel>();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}