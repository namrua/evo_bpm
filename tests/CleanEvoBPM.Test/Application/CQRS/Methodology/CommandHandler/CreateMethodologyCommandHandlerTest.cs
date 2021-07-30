using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.Common.Exceptions;
using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.Models.Methodology;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.Methodology
{
    public partial class MethodologyTest
    {
        [Theory]
        [MemberData(nameof(CreateMethodologyDataValid))]
        public async Task CreateMethodology_Success_UniqueCheckReturnsNull(CreateMethodologyCommand command)
        {
            _methodologyDataServiceMock
                .Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(null as IEnumerable<MethodologyResponseModel>));

            _methodologyDataServiceMock
                .Setup(x => x.CreateMethodology(It.IsAny<CreateMethodologyCommand>()))
                .Returns(Task.FromResult(GenericResponse.SuccessResult()));

            var result = await _createMethodologyCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
            _methodologyDataServiceMock.Verify(x => x.CreateMethodology(It.IsAny<CreateMethodologyCommand>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateMethodologyDataValid))]
        public async Task CreateMethodology_Success_UniqueCheckReturnsEmptyList(CreateMethodologyCommand command)
        {
            _methodologyDataServiceMock
                .Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(new List<MethodologyResponseModel>() as IEnumerable<MethodologyResponseModel>));

            _methodologyDataServiceMock
                .Setup(x => x.CreateMethodology(It.IsAny<CreateMethodologyCommand>()))
                .Returns(Task.FromResult(GenericResponse.SuccessResult()));

            var result = await _createMethodologyCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result.Success);
            _methodologyDataServiceMock.Verify(x => x.CreateMethodology(It.IsAny<CreateMethodologyCommand>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(CreateMethodologyDataValid))]
        public async Task CreateMethodology_Failure_NameNotUnique(CreateMethodologyCommand command)
        {
            var fetchResultStub = new List<MethodologyResponseModel>
            {
                new MethodologyResponseModel {
                    Id = Guid.NewGuid(),
                    MethodologyName = "Some name",
                    Description = "Some description",
                    RecordStatus = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                }
            };

            _methodologyDataServiceMock
                .Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(fetchResultStub as IEnumerable<MethodologyResponseModel>));

            var exception = await Assert.ThrowsAsync<CustomValidationException>(
                () => _createMethodologyCommandHandler.Handle(command, new CancellationToken()));

            Assert.Equal(1, exception.Errors.Count);
            Assert.True(exception.Errors.ContainsKey("MethodologyName"));
            Assert.Equal("Methodology Name must be unique", exception.Errors["MethodologyName"][0]);

            _methodologyDataServiceMock.Verify(x => x.CreateMethodology(It.IsAny<CreateMethodologyCommand>()), Times.Never);
        }
    }
}
