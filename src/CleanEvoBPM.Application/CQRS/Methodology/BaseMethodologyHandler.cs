using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Methodology
{
    public class BaseMethodologyHandler
    {
        public readonly IMethodologyDataService _methodologyDataService;
        public BaseMethodologyHandler(IMethodologyDataService methodologyDataService)
        {
            _methodologyDataService = methodologyDataService;
        }
    }
}
