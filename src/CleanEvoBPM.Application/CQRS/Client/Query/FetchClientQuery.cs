using CleanEvoBPM.Application.Models.Client;
using MediatR;
using System.Collections.Generic;

namespace CleanEvoBPM.Application.CQRS.Client.Query
{
    public class FetchClientQuery : IRequest<IEnumerable<ClientResponseModel>>
    {
        public string OrderBy { get; set; }
        public string Search { get; set; }
    }
}