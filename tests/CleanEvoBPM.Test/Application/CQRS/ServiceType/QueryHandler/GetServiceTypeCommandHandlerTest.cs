using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.Models.ServiceType;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ServiceType
{
    partial class ServiceTypeTest
    {
        [Theory]
        [MemberData(nameof(GetServiceTypeData))]
        public async Task GetServiceType_Success_DataReturnsNull(GetServiceTypeQuery query)
        {
            _ServiceTypeDataServiceMock
                .Setup(x => x.FetchServiceType(It.IsAny<GetServiceTypeQuery>()))
                .Returns(Task.FromResult(null as IEnumerable<ServiceTypeResponseModel>));

            var result = await _getServiceTypeQueryHandler.Handle(query, new CancellationToken());

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(GetServiceTypeData))]
        public async Task GetServiceType_Success_DataReturnsNotEmptyList(GetServiceTypeQuery query)
        {
            var returnedData = new List<ServiceTypeResponseModel> {
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
                .Returns(Task.FromResult(returnedData as IEnumerable<ServiceTypeResponseModel>));

            var result = await _getServiceTypeQueryHandler.Handle(query, new CancellationToken());

            Assert.Equal(returnedData.Count, result.Count());

            for (int i = 0; i < returnedData.Count; i++)
            {
                Assert.Equal(returnedData[i].ServiceTypeName, result.ElementAt(i).ServiceTypeName);
                Assert.Equal(returnedData[i].Description, result.ElementAt(i).Description);
                Assert.Equal(returnedData[i].RecordStatus, result.ElementAt(i).RecordStatus);
                Assert.Equal(returnedData[i].CreatedDate, result.ElementAt(i).CreatedDate);
                Assert.Equal(returnedData[i].UpdatedDate, result.ElementAt(i).UpdatedDate);
            }
        }
    }
}
