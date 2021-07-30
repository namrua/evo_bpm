using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Status.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Status;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Status.QueryHandler
{
    public class GetStatusDetailsHandler : BaseStatusHandler,IRequestHandler<GetStatusDetailsQuery, GenericResponse<StatusResponseModel>>
    {
        public GetStatusDetailsHandler(IStatusDataService statusDataService) : base(statusDataService)
        {
        }

        public async Task<GenericResponse<StatusResponseModel>> Handle(GetStatusDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _statusDataService.GetStatusDetails(request);
        }
    }
}