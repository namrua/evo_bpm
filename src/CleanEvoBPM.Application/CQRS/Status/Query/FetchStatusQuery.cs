using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Models.Status;
using MediatR;

namespace CleanEvoBPM.Application.CQRS.Status.Query
{
    public class FetchStatusQuery : IRequest<GenericListResponse<StatusResponseModel>>
    {
        #nullable enable
        public string? OrderBy { get; set; }
        public string? Search { get; set; }
    }
}