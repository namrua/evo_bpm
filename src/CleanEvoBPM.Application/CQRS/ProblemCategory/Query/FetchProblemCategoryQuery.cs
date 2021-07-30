using CleanEvoBPM.Application.Models.ProblemCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProblemCategory.Query
{
    public class FetchProblemCategoryQuery : IRequest<IEnumerable<ProblemCategoryResponseModel>>
    {
        public string Search { get; set; }
        public bool? Active { get; set; }
    }
}
