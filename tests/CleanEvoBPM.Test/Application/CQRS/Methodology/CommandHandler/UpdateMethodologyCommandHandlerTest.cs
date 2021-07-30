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
        [MemberData(nameof(UpdateMethodologyDataValid))]
        public async Task UpdateMethodology_Success_UniqueCheckReturnsEmptyList(UpdateMethodologyCommand command)
        {
            _methodologyDataServiceMock
                .Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(new List<MethodologyResponseModel>() as IEnumerable<MethodologyResponseModel>));

            _methodologyDataServiceMock
                .Setup(x => x.UpdateMethodology(It.IsAny<UpdateMethodologyCommand>()))
                .Returns(Task.FromResult(true));

            var result = await _updateMethodologyCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result);
            _methodologyDataServiceMock.Verify(x => x.UpdateMethodology(It.IsAny<UpdateMethodologyCommand>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UpdateMethodologyDataValid))]
        public async Task UpdateMethodology_Success_UniqueCheckReturnsSameEntity(UpdateMethodologyCommand command)
        {
            var fetchResultStub = new List<MethodologyResponseModel>
            {
                new MethodologyResponseModel { Id = command.Id }
            };

            _methodologyDataServiceMock
                .Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(fetchResultStub as IEnumerable<MethodologyResponseModel>));

            _methodologyDataServiceMock
                .Setup(x => x.UpdateMethodology(It.IsAny<UpdateMethodologyCommand>()))
                .Returns(Task.FromResult(true));

            var result = await _updateMethodologyCommandHandler.Handle(command, new CancellationToken());

            Assert.True(result);
            _methodologyDataServiceMock.Verify(x => x.UpdateMethodology(It.IsAny<UpdateMethodologyCommand>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(UpdateMethodologyDataValid))]
        public async Task UpdateMethodology_Failure_NameNotUnique(UpdateMethodologyCommand command)
        {
            var fetchResultStub = new List<MethodologyResponseModel>
            {
                new MethodologyResponseModel
                {
                    Id = Guid.NewGuid(),
                    MethodologyName = command.MethodologyName
                }
            };

            _methodologyDataServiceMock
                .Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(fetchResultStub as IEnumerable<MethodologyResponseModel>));

            var exception = await Assert.ThrowsAsync<CustomValidationException>(
                () => _updateMethodologyCommandHandler.Handle(command, new CancellationToken()));

            Assert.Equal(1, exception.Errors.Count);
            Assert.True(exception.Errors.ContainsKey("MethodologyName"));
            Assert.Equal("Methodology Name must be unique", exception.Errors["MethodologyName"][0]);

            _methodologyDataServiceMock.Verify(x => x.CreateMethodology(It.IsAny<CreateMethodologyCommand>()), Times.Never);
        }
    }
}
