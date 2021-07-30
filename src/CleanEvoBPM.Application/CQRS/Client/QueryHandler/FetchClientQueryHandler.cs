using CleanEvoBPM.Application.CQRS.Client.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Client;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Client.QueryHandler
{
    public class FetchClientQueryHandler : BaseClientHandler,
        IRequestHandler<FetchClientQuery, IEnumerable<ClientResponseModel>>
    {
        public FetchClientQueryHandler(IClientDataService clientDataService) : base(clientDataService) { }

        public async Task<IEnumerable<ClientResponseModel>> Handle(FetchClientQuery request, CancellationToken cancellationToken)
        {
            var result = await base._clientDataService.GetClients(request);
            return result;
        }
    }
}