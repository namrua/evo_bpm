using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Status.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Project;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Status.CommandHandler
{
    public class DeleteStatusCommandHandler : BaseStatusHandler, IRequestHandler<DeleteStatusCommand, GenericResponse>
    {
        public readonly IGenericDataService<ProjectMasterDataToDelete> _genericDataService;
        public DeleteStatusCommandHandler(IStatusDataService serviceTypeDataService,
            IGenericDataService<ProjectMasterDataToDelete> genericDataService)
            : base(serviceTypeDataService)
        {
            _genericDataService = genericDataService;
        }

        public async Task<GenericResponse> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
        {
            var checkList = await _genericDataService.GetAll(TableName.Project);
            if (checkList.Any(x => x.StatusId == request.Id))
                return new GenericResponse
                {
                    Code = 400,
                    Success = false,
                    Message = ValidateMessage.DeleteMasterDataFailed
                };
            var deleteResult = await _statusDataService.DeleteStatus(request.Id);
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