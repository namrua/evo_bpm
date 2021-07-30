using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProjectLLBP
{
    public class BaseProjectLLBPHandle
    {
        public readonly IProjectLLBPDataService _projectLLBPDataService;
        public BaseProjectLLBPHandle(IProjectLLBPDataService projectLLBPDataService)
        {
            _projectLLBPDataService = projectLLBPDataService;
        }
    }
}
