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
        [MemberData(nameof(UpdateServiceTypeDataValid))]
        public async Task UpdateServiceType_Success_UniqueCheckReturnsEmptyList(UpdateServiceTypeCommand command)
        {
            _ServiceTypeDataServiceMock
                .Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(new List<ServiceTypeResponseModel>() as IEnumerable<ServiceTypeResponseModel>));

            _ServiceTypeDataServiceMock
                .Setup(x => x.UpdateServiceType(It.IsAny<UpdateServiceTypeCommand>()))
                .Returns(Task.FromResult(true));

            var result = await _updateServiceTypeCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result);
            _ServiceTypeDataServiceMock.Verify(x => x.UpdateServiceType(It.IsAny<UpdateServiceTypeCommand>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UpdateServiceTypeDataValid))]
        public async Task UpdateServiceType_Success_UniqueCheckReturnsSameEntity(UpdateServiceTypeCommand command)
        {
            var fetchResultStub = new List<ServiceTypeResponseModel>
            {
                new ServiceTypeResponseModel { Id = command.Id }
            };

            _ServiceTypeDataServiceMock
                .Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(fetchResultStub as IEnumerable<ServiceTypeResponseModel>));

            _ServiceTypeDataServiceMock
                .Setup(x => x.UpdateServiceType(It.IsAny<UpdateServiceTypeCommand>()))
                .Returns(Task.FromResult(true));

            var result = await _updateServiceTypeCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result);
            _ServiceTypeDataServiceMock.Verify(x => x.UpdateServiceType(It.IsAny<UpdateServiceTypeCommand>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UpdateServiceTypeDataValid))]
        public async Task UpdateServiceType_Failure_NameNotUnique(UpdateServiceTypeCommand command)
        {
            var fetchResultStub = new List<ServiceTypeResponseModel>
            {
                new ServiceTypeResponseModel
                {
                    Id = Guid.NewGuid(),
                    ServiceTypeName = command.ServiceTypeName
                }
            };

            _ServiceTypeDataServiceMock
                .Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(fetchResultStub as IEnumerable<ServiceTypeResponseModel>));

            var exception = await Assert.ThrowsAsync<CustomValidationException>(
                () => _updateServiceTypeCommandHandler.Handle(command, new CancellationToken()));

            Assert.Equal(1, exception.Errors.Count);
            Assert.True(exception.Errors.ContainsKey("ServiceTypeName"));
            Assert.Equal("Service Type Name must be unique", exception.Errors["ServiceTypeName"][0]);

            _ServiceTypeDataServiceMock.Verify(x => x.CreateServiceType(It.IsAny<CreateServiceTypeCommand>()), Times.Never);
        }
    }
}
