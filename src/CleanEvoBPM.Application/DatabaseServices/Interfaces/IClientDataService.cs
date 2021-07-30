using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Client.Command;
using CleanEvoBPM.Application.CQRS.Client.Query;
using CleanEvoBPM.Application.Models.Client;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IClientDataService
    {
        Task<IEnumerable<ClientResponseModel>> GetClients(FetchClientQuery request);
        Task<ClientResponseModel> GetClient(GetClientDetailQuery request);
        Task<ClientResponseModel> GetClientByName(string name);
        Task<GenericResponse<ClientResponseModel>> CreateClient(CreateClientCommand request);
        Task<GenericResponse> UpdateClient(UpdateClientCommand requets);
        Task<bool> DeleteClient(DeleteClientCommand requets);
    }
}