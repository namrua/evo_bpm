using CleanEvoBPM.Application.Models.Technology;
using MediatR;
using System.Collections.Generic;

namespace CleanEvoBPM.Application.CQRS.Technology.Query
{
    public class FetchTechnologyQuery : IRequest<IEnumerable<TechnologyResponseModel>>
    {
        //  public int MaxPageSize { get; set; } = 20;
        //  public int PageNumber { get; set; } = 1;
        //  public string OrderBy { get; set; }
        public string Search { get; set; }
        public bool? Active {get;set;}
    }
}
