using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Role
{
    public class BaseRoleHandler
    {
        public readonly IRoleService _roleService;
        public BaseRoleHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
    }
}
