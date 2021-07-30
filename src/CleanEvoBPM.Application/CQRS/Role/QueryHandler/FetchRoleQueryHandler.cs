using CleanEvoBPM.Application.CQRS.Role.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Role.QueryHandler
{
    public class FetchRoleQueryHandler:BaseRoleHandler,IRequestHandler<FetchRoleQuery, IEnumerable<RoleResponseModel>>
    {
        public FetchRoleQueryHandler(IRoleService roleService):base(roleService)
        {

        }

        public async Task<IEnumerable<RoleResponseModel>> Handle(FetchRoleQuery request, CancellationToken cancellationToken)
        {
            return await _roleService.Fetch();
        }
    }
}
