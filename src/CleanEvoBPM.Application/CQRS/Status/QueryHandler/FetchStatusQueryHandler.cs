using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Status.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Status;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Status.QueryHandler
{
    public class FetchStatusQueryHandler : BaseStatusHandler,
        IRequestHandler<FetchStatusQuery, GenericListResponse<StatusResponseModel>>
    {
        public FetchStatusQueryHandler(IStatusDataService statusDataService) : base(statusDataService)
        {
        }

        public async Task<GenericListResponse<StatusResponseModel>> Handle(FetchStatusQuery request, CancellationToken cancellationToken)
        {
            return await _statusDataService.FetchStatus();
        }
    }
}