using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.Project.Command;
using CleanEvoBPM.Application.CQRS.Project.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Project.CommandHandler
{
    public class DeleteProjectCommandHandler : BaseProjectHandler,IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly INotificationDispatcher _dispatcher;
        public DeleteProjectCommandHandler(IProjectDataService projectDataService,
            IBusinessDomainDataService businessDomainDataService,
            IClientDataService clientDataService,
            INotificationDispatcher dispatcher) 
            : base(projectDataService, 
                  businessDomainDataService,
                  clientDataService)
        {
            _dispatcher = dispatcher;
        }
        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
           var result =  await _projectDataService.DeleteProject(request.ProjectID);
            await _dispatcher.Push(new DeleteProjectLog(request.ProjectID));
            return result;
        }
    }
}
