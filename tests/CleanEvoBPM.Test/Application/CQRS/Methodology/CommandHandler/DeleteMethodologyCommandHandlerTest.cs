using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Application.Models.ProjectMethodology;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.Methodology
{
    public partial class MethodologyTest
    {
        [Fact]
        public async Task HandleDeleteMethology_DeleteMasterDataFailed_FailedGenericResponse()
        {
            var methodologyId = Guid.NewGuid();
            var projectBusinessDomains = new List<ProjectMethodologyModel>
            {
                new ProjectMethodologyModel
                {
                    MethodologyId = methodologyId,
                    ProjectId = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteMethodologyCommand
            {
                Id = methodologyId
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectMethodology))
            .Returns(Task.FromResult(projectBusinessDomains));
            var result = await _deleteMethodologyCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed, result.Message);
        }

        [Fact]
        public async Task HandleDeleteMethology_NotFound_FailedGenericResponse()
        {
            var methodologyId = Guid.NewGuid();
            var projectBusinessDomains = new List<ProjectMethodologyModel>
            {
                new ProjectMethodologyModel
                {
                    MethodologyId = methodologyId,
                    ProjectId = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteMethodologyCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectMethodology)).Returns(Task.FromResult(projectBusinessDomains));

            var result = await _deleteMethodologyCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.NotFound, result.Message);
        }
        [Fact]
        public async Task HandleDeleteMethology_Success_SuccessGenericResponse()
        {
            var businessDomainId = Guid.NewGuid();
            var projectBusinessDomains = new List<ProjectMethodologyModel>
            {
                new ProjectMethodologyModel
                {
                    MethodologyId = businessDomainId,
                    ProjectId = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteMethodologyCommand
            {
                Id = Guid.NewGuid()
            };
            _mockGenericDataService.Setup(x => x.GetAll(TableName.ProjectMethodology)).Returns(Task.FromResult(projectBusinessDomains));
            _methodologyDataServiceMock.Setup(x => x.DeleteMethodology(It.IsAny<Guid>()))
                                            .Returns(Task.FromResult(true));

            var result = await _deleteMethodologyCommandHandler.Handle(request, new CancellationToken());
            Assert.True(result.Success);
            Assert.Equal(ValidateMessage.DeleteSucess, result.Message);
        }
    }
}
