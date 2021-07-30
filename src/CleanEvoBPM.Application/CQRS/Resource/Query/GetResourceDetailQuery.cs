using CleanEvoBPM.Application.Models.Resource;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.Resource.Query
{
    public class GetResourceDetailQuery:IRequest<ResourceResponseModel>
    {
        public Guid Id { get; set; }
        public string ProjectId { get; set; }
        public string ResourceId { get; set; }
    }
}
