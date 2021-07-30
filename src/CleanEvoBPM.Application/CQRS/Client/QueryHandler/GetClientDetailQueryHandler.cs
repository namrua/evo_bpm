using CleanEvoBPM.Application.CQRS.Client.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.Client.QueryHandler
{
    public class GetClientDetailQueryHandler : IRequestHandler<GetClientDetailQuery, ClientResponseModel>
    {
        private readonly IClientDataService _clientDataService;
        public GetClientDetailQueryHandler(IClientDataService clientDataService)
        {
            _clientDataService = clientDataService;
        }
        public async Task<ClientResponseModel> Handle(GetClientDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientDataService.GetClient(request);
            return result;
        }
    }
}
