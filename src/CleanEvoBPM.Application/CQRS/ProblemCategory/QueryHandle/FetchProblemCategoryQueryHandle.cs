using CleanEvoBPM.Application.CQRS.ProblemCategory.Query;
using CleanEvoBPM.Application.CQRS.Technology;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProblemCategory;
using CleanEvoBPM.Application.Models.Technology;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.CQRS.ProblemCategory.QueryHandle
{
    public class FetchProblemCategoryQueryHandle : BaseProblemCategoryHandle, IRequestHandler<FetchProblemCategoryQuery, IEnumerable<ProblemCategoryResponseModel>>
    {
        public FetchProblemCategoryQueryHandle(IProblemCategoryService problemCategoryService) : base(problemCategoryService)
        {
        }
        public async Task<IEnumerable<ProblemCategoryResponseModel>> Handle(FetchProblemCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _problemCategoryService.Fetch(request);
            return result;
        }

      
    }
}
