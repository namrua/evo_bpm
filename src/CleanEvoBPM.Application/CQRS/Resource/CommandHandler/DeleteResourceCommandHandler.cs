using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Resource.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Resource.CommandHandler
{
    public class DeleteResourceCommandHandler : BaseResourceHandler, IRequestHandler<DeleteResourceCommand, GenericResponse>
    {
        public DeleteResourceCommandHandler(IResourceService resourceService, IRoleService roleService) : base(resourceService, roleService)
        {

        }

        public async Task<GenericResponse> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            return new GenericResponse
            {
                Success = await _resourceService.Delete(request)
            };
        }
    }
}
