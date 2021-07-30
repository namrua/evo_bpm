using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.DeliveryLocation
{
    public class BaseDeliveryLocationHandler
    {
        public readonly IDeliveryLocationDataService _deliveryLocationDataService;
        public BaseDeliveryLocationHandler(IDeliveryLocationDataService deliveryLocationDataService)
        {
            _deliveryLocationDataService = deliveryLocationDataService;
        }
    }
}
