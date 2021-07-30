using CleanEvoBPM.Application.Models.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponseModel>> Fetch();
    }
}
