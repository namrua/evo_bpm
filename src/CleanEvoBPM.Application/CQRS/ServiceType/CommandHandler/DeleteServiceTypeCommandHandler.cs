using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using MediatR;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Models.Project;
using System.Linq;
using CleanEvoBPM.Domain;
using CleanEvoBPM.Application.CQRS.ServiceType.Event;

namespace CleanEvoBPM.Application.CQRS.ServiceType.CommandHandler
{
    public class DeleteServiceTypeCommandHandler : BaseServiceTypeHandler, IRequestHandler<DeleteServiceTypeCommand, GenericResponse>
    {
        private readonly INotificationDispatcher _dispatcher;
        public readonly IGenericDataService<ProjectMasterDataToDelete> _genericDataService;
        public DeleteServiceTypeCommandHandler(IServiceTypeDataService serviceTypeDataService,
            IGenericDataService<ProjectMasterDataToDelete> genericDataService,
            INotificationDispatcher dispatcher)
            : base(serviceTypeDataService)
        {
            _genericDataService = genericDataService;
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(DeleteServiceTypeCommand request, CancellationToken cancellationToken)
        {
            var checkList = await _genericDataService.GetAll(TableName.Project);
            if (checkList.Any(x => x.ServiceTypeId == request.Id))
                return new GenericResponse
                {
                    Code = 400,
                    Success = false,
                    Message = ValidateMessage.DeleteMasterDataFailed
                };
            await _dispatcher.Push(new DeleteServiceTypeLog(request.Id));
            var deleteResult = await _serviceTypeDataService.DeleteServiceType(request.Id);
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
