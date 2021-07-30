using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Common.Exceptions;
using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.Models.ServiceType;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ServiceType
{
    public partial class ServiceTypeTest
    {
        [Theory]
        [MemberData(nameof(CreateServiceTypeDataValid))]
        public async Task CreateServiceType_Success_UniqueCheckReturnsNull(CreateServiceTypeCommand command)
        {
            _ServiceTypeDataServiceMock
                .Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(null as IEnumerable<ServiceTypeResponseModel>));

            _ServiceTypeDataServiceMock
                .Setup(x => x.CreateServiceType(It.IsAny<CreateServiceTypeCommand>()))
                .Returns(Task.FromResult(GenericResponse.SuccessResult()));

            var result = await _createServiceTypeCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
            _ServiceTypeDataServiceMock.Verify(x => x.CreateServiceType(It.IsAny<CreateServiceTypeCommand>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateServiceTypeDataValid))]
        public async Task CreateServiceType_Success_UniqueCheckReturnsEmptyList(CreateServiceTypeCommand command)
        {
            _ServiceTypeDataServiceMock
                .Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(new List<ServiceTypeResponseModel>() as IEnumerable<ServiceTypeResponseModel>));

            _ServiceTypeDataServiceMock
                .Setup(x => x.CreateServiceType(It.IsAny<CreateServiceTypeCommand>()))
                .Returns(Task.FromResult(GenericResponse.SuccessResult()));

            var result = await _createServiceTypeCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
            _ServiceTypeDataServiceMock.Verify(x => x.CreateServiceType(It.IsAny<CreateServiceTypeCommand>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateServiceTypeDataValid))]
        public async Task CreateServiceType_Failure_NameNotUnique(CreateServiceTypeCommand command)
        {
            var fetchResultStub = new List<ServiceTypeResponseModel>
            {
                new ServiceTypeResponseModel {
                    Id = Guid.NewGuid(),
                    ServiceTypeName = "Some name",
                    Description = "Some description",
                    RecordStatus = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                }
            };

            _ServiceTypeDataServiceMock
                .Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(fetchResultStub as IEnumerable<ServiceTypeResponseModel>));

            var exception = await Assert.ThrowsAsync<CustomValidationException>(
                () => _createServiceTypeCommandHandler.Handle(command, new CancellationToken()));

            Assert.Equal(1, exception.Errors.Count);
            Assert.True(exception.Errors.ContainsKey("ServiceTypeName"));
            Assert.Equal("Service Type Name must be unique", exception.Errors["ServiceTypeName"][0]);

            _ServiceTypeDataServiceMock.Verify(x => x.CreateServiceType(It.IsAny<CreateServiceTypeCommand>()), Times.Never);
        }
    }
}
