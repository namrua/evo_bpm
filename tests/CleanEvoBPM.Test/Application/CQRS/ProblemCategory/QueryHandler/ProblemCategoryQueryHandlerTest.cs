using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Query;
using CleanEvoBPM.Application.Models.ProblemCategory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ProblemCategory
{
    public partial class ProblemCategoryTest
    {
        [Theory]
        [MemberData(nameof(GetFetchProblemCategoryQueryData))]
        public void Handle_FetchProblemCategory(FetchProblemCategoryQuery param)
        {
            var actualParam = new FetchProblemCategoryQuery();
            var listDataReturn = GetDataReturnFetchProblemCategory;
            _problemCategoryService.Setup(x => x.Fetch(It.IsAny<FetchProblemCategoryQuery>()))
                .Returns(Task.FromResult(listDataReturn));

            _problemCategoryService.Setup(x => x.Fetch(It.IsAny<FetchProblemCategoryQuery>()))
                .Callback<FetchProblemCategoryQuery>((input) =>
                {
                    actualParam = input;
                });
            var result = _fetchProblemCategoryQueryHandle.Handle(param, new CancellationToken());
            _problemCategoryService.Verify(x => x.Fetch(It.IsAny<FetchProblemCategoryQuery>()), Times.AtLeastOnce);

            Assert.Equal(typeof(Task<IEnumerable<ProblemCategoryResponseModel>>), result.GetType());
            Assert.Equal(param.Search, actualParam.Search);
            Assert.Equal(param.Active, actualParam.Active);
            Assert.False(listDataReturn.Where(x => x.Name.Contains(param.Search)).Count() > 1);
        }
    }
}
