using System;
using System.Collections.Generic;
using System.Text;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;

namespace CleanEvoBPM.Application.CQRS.ServiceType
{
    public class BaseServiceTypeHandler
    {
        public readonly IServiceTypeDataService _serviceTypeDataService;
        public BaseServiceTypeHandler(IServiceTypeDataService serviceTypeDataService)
        {
            _serviceTypeDataService = serviceTypeDataService;
        }
    }
}
