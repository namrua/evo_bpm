using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ProblemCategory.Command;
using CleanEvoBPM.Application.Models.ProjectLLBP;
using Microsoft.Extensions.DependencyInjection;
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
        [MemberData(nameof(GetUpdateProblemCategoryData))]
        public async Task Handle_UpdateProblemCategory(UpdateProblemCategoryCommand param)
        {
            var actualParam = new UpdateProblemCategoryCommand();
            _problemCategoryService.Setup(x => x.Update(It.IsAny<UpdateProblemCategoryCommand>())).Returns(Task.FromResult(true));
            _problemCategoryService.Setup(x => x.Update(It.IsAny<UpdateProblemCategoryCommand>()))
              .Callback<UpdateProblemCategoryCommand>((input) =>
              {
                  actualParam = input;
              });

            var result = await _updateProblemCategoryCommandHandler.Handle(param, new CancellationToken());

            Assert.Equal(typeof(bool), result.GetType());
            Assert.Equal(param.Id, actualParam.Id);
            Assert.Equal(param.Name, actualParam.Name);
            Assert.Equal(param.Description, actualParam.Description);
            Assert.Equal(param.IsActived, actualParam.IsActived);
            Assert.Equal(param.CreatedDate, actualParam.CreatedDate);
            Assert.Equal(param.CreatedBy, actualParam.CreatedBy);
            Assert.Equal(param.DeleteFlag, actualParam.DeleteFlag);
            _problemCategoryService.Verify(x => x.Update(It.IsAny<UpdateProblemCategoryCommand>()), Times.AtLeastOnce);
        }

        [Theory]
        [MemberData(nameof(GetCreateProblemCategoryData))]
        public async Task Handle_CreateProblemCategory(CreateProblemCategoryCommand param)
        {
            var actualParam = new CreateProblemCategoryCommand();
            _problemCategoryService.Setup(x => x.Create(It.IsAny<CreateProblemCategoryCommand>())).Returns(Task.FromResult(true));
            _problemCategoryService.Setup(x => x.Create(It.IsAny<CreateProblemCategoryCommand>()))
              .Callback<CreateProblemCategoryCommand>((input) =>
              {
                  actualParam = input;
              });

            var result = await _createProblemCategoryCommandHandler.Handle(param, new CancellationToken());

            Assert.Equal(typeof(bool), result.GetType());
            Assert.Equal(param.Name, actualParam.Name);
            Assert.Equal(param.Description, actualParam.Description);
            Assert.Equal(param.IsActived, actualParam.IsActived);
            Assert.Equal(param.CreatedDate, actualParam.CreatedDate);
            Assert.Equal(param.DeleteFlag, actualParam.DeleteFlag);
            _problemCategoryService.Verify(x => x.Create(It.IsAny<CreateProblemCategoryCommand>()), Times.AtLeastOnce);
        }

         [Fact]
        public async Task HandleDeleteProblemCategory_DeleteMasterDataFailed_FailedGenericResponse()
        {
            var problemId = Guid.NewGuid();
            var problemCate = new List<ProjectLLBPModel>
            {
                new ProjectLLBPModel
                {
                    ProblemCategory = problemId,
                }
            }.AsEnumerable();
            var request = new DeleteProblemCategoryCommand
            {
                Id = problemId
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectLLBP))
            .Returns(Task.FromResult(problemCate));
            var result = await _deleteProblemCategoryCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed, result.Message);
        }

        [Fact]
        public async Task HandleDeleteProblemCategory_NotFound_FailedGenericResponse()
        {
            var problemId = Guid.NewGuid();
            var problemCate = new List<ProjectLLBPModel>
            {
                new ProjectLLBPModel
                {
                    ProblemCategory = problemId,
                }
            }.AsEnumerable();
            var request = new DeleteProblemCategoryCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectLLBP)).Returns(Task.FromResult(problemCate));

            var result = await _deleteProblemCategoryCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.NotFound, result.Message);
        }
        [Fact]
        public async Task HandleDeleteProblemCategory_Success_SuccessGenericResponse()
        {
            var problemId = Guid.NewGuid();
            var problemCate = new List<ProjectLLBPModel>
            {
                new ProjectLLBPModel
                {
                    ProblemCategory = problemId,
                }
            }.AsEnumerable();
            var request = new DeleteProblemCategoryCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectLLBP)).Returns(Task.FromResult(problemCate));
            _problemCategoryService.Setup(x => x.Delete(It.IsAny<DeleteProblemCategoryCommand>()))
                                            .Returns(Task.FromResult(true));

            var result = await _deleteProblemCategoryCommandHandler.Handle(request, new CancellationToken());
            Assert.True(result.Success);
            Assert.Equal(ValidateMessage.DeleteSucess, result.Message);
        }
    }
}
