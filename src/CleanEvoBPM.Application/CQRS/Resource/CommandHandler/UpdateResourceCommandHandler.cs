using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Resource.Command;
using CleanEvoBPM.Application.CQRS.Role;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Resource.CommandHandler
{
    public class UpdateResourceCommandHandler:BaseResourceHandler, IRequestHandler<UpdateResourceCommand, GenericResponse>
    {
        public UpdateResourceCommandHandler(IResourceService resourceService, IRoleService roleService):base(resourceService, roleService)
        {

        }

        public async Task<GenericResponse> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            return new GenericResponse
            {
                Success = await _resourceService.Update(request)
            };
        }
    }
}
