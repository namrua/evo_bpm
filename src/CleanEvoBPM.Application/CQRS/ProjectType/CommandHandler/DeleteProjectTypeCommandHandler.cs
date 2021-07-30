using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.CQRS.ProjectType.Command;
using CleanEvoBPM.Application.CQRS.ProjectType.Event;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Domain;
using MediatR;
using CleanEvoBPM.Application.Models.Project;
using CleanEvoBPM.Application.Common;
using System.Linq;

namespace CleanEvoBPM.Application.CQRS.ProjectType.CommandHandler
{
    public class DeleteProjectTypeCommandHandler : BaseProjectTypeHandler, IRequestHandler<DeleteProjectTypeCommand, GenericResponse>
    {
        private readonly INotificationDispatcher _dispatcher;
        public readonly IGenericDataService<ProjectMasterDataToDelete> _genericDataService;
        public DeleteProjectTypeCommandHandler(IProjectTypeDataService projectTypeDataService,
            IGenericDataService<ProjectMasterDataToDelete> genericDataService,
            INotificationDispatcher dispatcher)
            : base(projectTypeDataService)
        {
            _genericDataService = genericDataService;
             _dispatcher = dispatcher;
        }
        
        public DeleteProjectTypeCommandHandler(IProjectTypeDataService projectTypeDataService,
            INotificationDispatcher dispatcher) : base(projectTypeDataService)
        {
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(DeleteProjectTypeCommand request, CancellationToken cancellationToken)
        {
            var checkList = await _genericDataService.GetAll(TableName.Project);
            if (checkList.Any(x => x.ProjectTypeId == request.Id))
                return new GenericResponse
                {
                    Code = 400,
                    Success = false,
                    Message = ValidateMessage.DeleteMasterDataFailed
                };
            await _dispatcher.Push(new DeleteProjectTypeLog(request.Id));
            var deleteResult = await _projectTypeDataService.DeleteProjectType(request.Id);
            if (!deleteResult)
                return new GenericResponse
                {
                    Code = 404,
                    Success = false,
                    Message = ValidateMessage.NotFound
                };
            return new GenericResponse
            {
                Code = 200,
                Success = true,
                Message = ValidateMessage.DeleteSucess
            };
        }
    }
}
