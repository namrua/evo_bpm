using System;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Status.Command;
using CleanEvoBPM.Application.CQRS.Status.Query;
using CleanEvoBPM.Application.Models.Status;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IStatusDataService
    {
        Task<GenericResponse> CreateStatus(CreateStatusCommand request);
        Task<bool> DeleteStatus(Guid id);
        Task<bool> UpdateStatus(UpdateStatusCommand request);
        //Task<bool> IsStatusExisted(string statusName);
        Task<GenericListResponse<StatusResponseModel>> FetchStatus();
        Task<GenericResponse<StatusResponseModel>> GetStatusDetails(GetStatusDetailsQuery query);
    }
}