using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Application.CQRS.ProjectType.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using MediatR;
namespace CleanEvoBPM.Application.CQRS.ProjectType.CommandHandler
{
    public class CreateProjectTypeCommandHandler : BaseProjectTypeHandler, IRequestHandler<CreateProjectTypeCommand, GenericResponse>
    {
        private readonly INotificationDispatcher _dispatcher;
        public CreateProjectTypeCommandHandler(IProjectTypeDataService projectTypeDataService,
            INotificationDispatcher dispatcher) : base(projectTypeDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(CreateProjectTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await base._projectTypeDataService.CreateProjectType(request);
            await _dispatcher.Push(new CreateProjectTypeLog(request));
            return result;
        }
    }
}
