using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Resource.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Resource;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Resource.CommandHandler
{
    public class CreateResourceCommandHandler:BaseResourceHandler, IRequestHandler<CreateResourceCommand, GenericResponse>
    {
        public CreateResourceCommandHandler(IResourceService resourceService, IRoleService roleService) : base(resourceService, roleService)
        {

        }

        public async Task<GenericResponse> Handle(CreateResourceCommand command, CancellationToken cancellationToken)
        {
            var result = await _resourceService.CreateResource(command);
            return new GenericResponse
            {
                Success = result
            };
        }
    }
}
