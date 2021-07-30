using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Resource
{
    public class BaseResourceHandler
    {
        public readonly IResourceService _resourceService;
        public readonly IRoleService _roleService;
        public BaseResourceHandler(IResourceService resourceService, IRoleService roleService)
        {
            _resourceService = resourceService;
            _roleService = roleService;
        }
    }
}
