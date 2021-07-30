using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Query;
using CleanEvoBPM.Application.Models.ProblemCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ProblemCategory
{
    public partial class ProblemCategoryTest
    {
        public static TheoryData<UpdateProblemCategoryCommand> GetUpdateProblemCategoryData
        {
            get
            {
                var data = new TheoryData<UpdateProblemCategoryCommand>();
                data.Add(new UpdateProblemCategoryCommand
                {
                    Id = Guid.NewGuid(),
                    Name= "Communication",
                    Description= "This is Description",
                    UpdatedDate = DateTime.UtcNow,
                    UpdatedBy = "",
                    IsActived = true,
                    CreatedBy= "",
                    //DeletedBy = Guid.NewGuid(),
                    DeleteFlag = false
                });
                data.Add(new UpdateProblemCategoryCommand
                {
                    Id = Guid.NewGuid(),
                    Name = "Process",
                    Description = "This is Description",
                    UpdatedDate = DateTime.UtcNow,
                    UpdatedBy = "",
                    IsActived = false,
                    CreatedBy = "",
                    //DeletedBy = Guid.NewGuid().ToString(),
                    DeleteFlag = true
                });
                return data;
            }
        }
        
        public static TheoryData<CreateProblemCategoryCommand> GetCreateProblemCategoryData
        {
            get
            {
                var data = new TheoryData<CreateProblemCategoryCommand>();
                data.Add(new CreateProblemCategoryCommand
                {
                    Name = "Communication",
                    Description = "This is Description",
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = "",
                    IsActived = true,
                    CreatedBy = "",
                    DeleteFlag = false
                });
                data.Add(new CreateProblemCategoryCommand
                {
                    Name = "Process",
                    Description = "This is Description",
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = "",
                    IsActived = false,
                    CreatedBy = "",
                    DeleteFlag = true
                });
                return data;
            }
        }

        public static TheoryData<DeleteProblemCategoryCommand> GetDeleteProblemCategoryData
        {
            get
            {
                var data = new TheoryData<DeleteProblemCategoryCommand>();
                data.Add(new DeleteProblemCategoryCommand
                {
                    Id = Guid.NewGuid()
                });
                data.Add(new DeleteProblemCategoryCommand
                {
                    Id = Guid.NewGuid()
                });
                return data;
            }
        }

        public static TheoryData<FetchProblemCategoryQuery> GetFetchProblemCategoryQueryData
        {
            get
            {
                var data = new TheoryData<FetchProblemCategoryQuery>();
                data.Add(new FetchProblemCategoryQuery
                {
                    Search = "nam",
                    Active = true
                });
                data.Add(new FetchProblemCategoryQuery
                {
                    Search = "process",
                    Active = true
                });
                return data;
            }
        }

        public static IEnumerable<ProblemCategoryResponseModel> GetDataReturnFetchProblemCategory
        {
            get
            {
                var data = new List<ProblemCategoryResponseModel>();
                data.Add(new ProblemCategoryResponseModel
                {
                    Id = Guid.NewGuid(),
                    Name = "nam zua ne",
                    Description= "This is description",
                    IsActived = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = Guid.NewGuid().ToString(),
                    CreatedBy = Guid.NewGuid().ToString(),
                    DeleteFlag = false
                });
                data.Add(new ProblemCategoryResponseModel
                {
                    Id = Guid.NewGuid(),
                    Name = "tartarus",
                    Description = "This is description",
                    IsActived = true,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = Guid.NewGuid().ToString(),
                    CreatedBy = Guid.NewGuid().ToString(),
                    DeleteFlag = true
                });
                return data.AsEnumerable();
            }
        }
    }
}
