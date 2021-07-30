using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Query;
using CleanEvoBPM.Application.CQRS.Technology.Query;
using CleanEvoBPM.Application.Models.ProblemCategory;
using CleanEvoBPM.Application.Models.Technology;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanEvoBPM.Application.DatabaseServices.Interfaces
{
    public interface IProblemCategoryService
    {
        Task<IEnumerable<ProblemCategoryResponseModel>> Fetch(FetchProblemCategoryQuery query);
        Task<ProblemCategoryResponseModel> GetProblemCategoryDetail(GetProblemCategoryDetailQuery query);
        Task<bool> Create(CreateProblemCategoryCommand command);
        Task<bool> Update(UpdateProblemCategoryCommand request);
        Task<bool> Delete(DeleteProblemCategoryCommand request);

    }
}
