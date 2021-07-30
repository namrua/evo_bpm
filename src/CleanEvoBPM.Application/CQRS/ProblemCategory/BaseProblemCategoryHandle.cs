using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanEvoBPM.Application.CQRS.ProblemCategory
{
    public class BaseProblemCategoryHandle
    {
        public readonly IProblemCategoryService _problemCategoryService;
        public BaseProblemCategoryHandle(IProblemCategoryService problemCategoryService)
        {
            _problemCategoryService = problemCategoryService;
        }
    }
}
