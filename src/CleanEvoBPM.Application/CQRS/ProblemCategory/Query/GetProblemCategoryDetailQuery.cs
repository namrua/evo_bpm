using CleanEvoBPM.Application.Models.ProblemCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProblemCategory.Query
{
    public class GetProblemCategoryDetailQuery:IRequest<ProblemCategoryResponseModel>
    {
        public Guid? Id { get; set; }
        public String Name { get; set; }
    }
}
