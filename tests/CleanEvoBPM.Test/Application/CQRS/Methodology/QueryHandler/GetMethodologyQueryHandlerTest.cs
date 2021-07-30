using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.Models.Methodology;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.Methodology
{
    partial class MethodologyTest
    {
        [Theory]
        [MemberData(nameof(GetMethodologyData))]
        public async Task GetMethodology_Success_DataReturnsNull(GetMethodologyQuery query)
        {
            _methodologyDataServiceMock
                .Setup(x => x.FetchMethodology(It.IsAny<GetMethodologyQuery>()))
                .Returns(Task.FromResult(null as IEnumerable<MethodologyResponseModel>));

            var result = await _getMethodologyQueryHandler.Handle(query, new CancellationToken());

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(GetMethodologyData))]
        public async Task GetMethodology_Success_DataReturnsNotEmptyList(GetMethodologyQuery query)
        {
            var returnedData = new List<MethodologyResponseModel> {
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
                .Returns(Task.FromResult(returnedData as IEnumerable<MethodologyResponseModel>));

            var result = await _getMethodologyQueryHandler.Handle(query, new CancellationToken());

            Assert.Equal(returnedData.Count, result.Count());

            for (int i = 0; i < returnedData.Count; i++)
            {
                Assert.Equal(returnedData[i].MethodologyName, result.ElementAt(i).MethodologyName);
                Assert.Equal(returnedData[i].Description, result.ElementAt(i).Description);
                Assert.Equal(returnedData[i].RecordStatus, result.ElementAt(i).RecordStatus);
                Assert.Equal(returnedData[i].CreatedDate, result.ElementAt(i).CreatedDate);
                Assert.Equal(returnedData[i].UpdatedDate, result.ElementAt(i).UpdatedDate);
            }
        }
    }
}
