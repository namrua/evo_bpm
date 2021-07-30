using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Application.Models.Project;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ServiceType
{
    public partial class ServiceTypeTest
    {
        [Fact]
        public async Task HandleDeleteServiceType_DeleteMasterDataFailed_FailedGenericResponse()
        {
            var serviceTypeId = Guid.NewGuid();
            var serviceTypes = new List<ProjectMasterDataToDelete>
            {
                new ProjectMasterDataToDelete
                {
                    ServiceTypeId = serviceTypeId,
                    Id = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteServiceTypeCommand
            {
                Id = serviceTypeId
            };
            _genericDataServiceMock.Setup(x => x.GetAll(TableName.Project))
            .Returns(Task.FromResult(serviceTypes));
            var result = await _deleteServiceTypeCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed, result.Message);
        }

        [Fact]
        public async Task HandleDeleteServiceType_NotFound_FailedGenericResponse()
        {
            var serviceTypeId = Guid.NewGuid();
            var serviceTypes = new List<ProjectMasterDataToDelete>
            {
                new ProjectMasterDataToDelete
                {
                    ServiceTypeId = serviceTypeId,
                    Id = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteServiceTypeCommand
            {
                Id = Guid.NewGuid()
            };
            _genericDataServiceMock.Setup(x => x.GetAll(TableName.Project)).Returns(Task.FromResult(serviceTypes));

            var result = await _deleteServiceTypeCommandHandler.Handle(request, new CancellationToken());
            Assert.False(result.Success);
            Assert.Equal(ValidateMessage.NotFound, result.Message);
        }
        [Fact]
        public async Task HandleDeleteServiceType_Success_SuccessGenericResponse()
        {
            var serviceTypeId = Guid.NewGuid();
            var serviceTypes = new List<ProjectMasterDataToDelete>
            {
                new ProjectMasterDataToDelete
                {
                    ServiceTypeId = serviceTypeId,
                    Id = Guid.NewGuid()
                }
            }.AsEnumerable();
            var request = new DeleteServiceTypeCommand
            {
                Id = Guid.NewGuid()
            };
            _genericDataServiceMock.Setup(x => x.GetAll(TableName.Project)).Returns(Task.FromResult(serviceTypes));
            _ServiceTypeDataServiceMock.Setup(x => x.DeleteServiceType(It.IsAny<Guid>()))
                                            .Returns(Task.FromResult(true));

            var result = await _deleteServiceTypeCommandHandler.Handle(request, new CancellationToken());
            Assert.True(result.Success);
            Assert.Equal(ValidateMessage.DeleteSucess, result.Message);
        }
    }
}
