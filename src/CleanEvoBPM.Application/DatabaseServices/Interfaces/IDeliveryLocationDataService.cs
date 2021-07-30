using CleanEvoBPM.Application.CQRS.DeliveryLocation.Query;
using CleanEvoBPM.Application.Models.DeliveryLocation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IDeliveryLocationDataService
    {
        Task<DeliveryLocationResponseModel> GetById(Guid id);
        Task<IEnumerable<DeliveryLocationResponseModel>> Fetch(GetDeliveryLocationQuery request);
    }
}
