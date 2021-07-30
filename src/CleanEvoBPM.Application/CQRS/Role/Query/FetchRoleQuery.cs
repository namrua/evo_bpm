using CleanEvoBPM.Application.Models.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Role.Query
{
    public class FetchRoleQuery:IRequest<IEnumerable<RoleResponseModel>>
    {

    }
}
