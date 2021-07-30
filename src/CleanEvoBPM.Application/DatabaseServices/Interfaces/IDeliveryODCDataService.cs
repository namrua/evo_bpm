using CleanEvoBPM.Application.CQRS.DeliveryODC.Query;
using CleanEvoBPM.Application.Models.DeliveryODC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IDeliveryODCDataService
    {
        Task<DeliveryODCResponseModel> GetById(Guid id);
        Task<IEnumerable<DeliveryODCResponseModel>> Fetch(GetDeliveryODCQuery request);
    }
}
