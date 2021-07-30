using System;
using System.Collections.Generic;
using System.Text;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;

namespace CleanEvoBPM.Application.CQRS.ProjectType
{    
    public class BaseProjectTypeHandler
    {
        public readonly IProjectTypeDataService _projectTypeDataService;
        public BaseProjectTypeHandler(IProjectTypeDataService projectTypeDataService)
        {
            _projectTypeDataService = projectTypeDataService;
        }
    }
}
