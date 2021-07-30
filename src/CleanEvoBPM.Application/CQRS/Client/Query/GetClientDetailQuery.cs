using CleanEvoBPM.Application.Models.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Client.Query
{
    public class GetClientDetailQuery : IRequest<ClientResponseModel>
    {
        public Guid? Id { get; set; }
        public String Name {get;set;}
    }
}
