using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectMethodology;
using CleanEvoBPM.Application.CQRS.Methodology.Event;
using CleanEvoBPM.Domain;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Methodology.CommandHandler
{
    public class DeleteMethodologyCommandHandler : BaseMethodologyHandler, IRequestHandler<DeleteMethodologyCommand, GenericResponse>
    {
         private readonly INotificationDispatcher _dispatcher;
        public readonly IGenericDataService<ProjectMethodologyModel> _genericDataService;
        public DeleteMethodologyCommandHandler(IMethodologyDataService methodologyDataService,
            IGenericDataService<ProjectMethodologyModel> genericDataService,
            INotificationDispatcher dispatcher) 
            : base(methodologyDataService)
        {
            _genericDataService = genericDataService;
            _dispatcher = dispatcher;
        }
        public async Task<GenericResponse> Handle(DeleteMethodologyCommand request, CancellationToken cancellationToken)
        {
            var checkList = await _genericDataService.GetAll(TableName.ProjectMethodology);
            if (checkList.Any(x => x.MethodologyId == request.Id))
                return new GenericResponse
                {
                    Code = 400,
                    Success = false,
                    Message = ValidateMessage.DeleteMasterDataFailed
                };
            await _dispatcher.Push(new DeleteMethodologyLog(request.Id));
            var deleteResult = await _methodologyDataService.DeleteMethodology(request.Id);
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
