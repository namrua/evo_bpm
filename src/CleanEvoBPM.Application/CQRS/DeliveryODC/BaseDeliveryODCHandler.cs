using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.DeliveryODC
{
    public class BaseDeliveryODCHandler
    {
        public readonly IDeliveryODCDataService _deliveryODCDataService;
        public BaseDeliveryODCHandler(IDeliveryODCDataService deliveryODCDataService) 
        {
            _deliveryODCDataService = deliveryODCDataService;
        }
    }
}
