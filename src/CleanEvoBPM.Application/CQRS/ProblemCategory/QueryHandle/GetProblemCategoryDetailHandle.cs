using CleanEvoBPM.Application.CQRS.ProblemCategory.Query;
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
    public class GetProblemCategoryDetailHandle:IRequestHandler<GetProblemCategoryDetailQuery, ProblemCategoryResponseModel>
    {
        private readonly IProblemCategoryService _problemCategoryService;
        public GetProblemCategoryDetailHandle(IProblemCategoryService problemCategoryService)
        {
            _problemCategoryService = problemCategoryService;
        }

        public async Task<ProblemCategoryResponseModel> Handle(GetProblemCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _problemCategoryService.GetProblemCategoryDetail(request);
            return result;
        }

    }
}
