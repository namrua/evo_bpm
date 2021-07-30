using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.Models.ServiceType;
using System;
using System.Collections.Generic;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ServiceType
{
    public partial class ServiceTypeTest
    {
        public static TheoryData<GetServiceTypeQuery> GetServiceTypeData
        {
            get
            {
                var data = new TheoryData<GetServiceTypeQuery>
                {
                    new GetServiceTypeQuery(),
                    new GetServiceTypeQuery
                    {
                        RecordStatus = true,
                        Id = Guid.NewGuid(),
                        Search = "Some text"
                    }
                };

                return data;
            }
        }

        public static TheoryData<List<ServiceTypeResponseModel>> GetResultMocked = new TheoryData<List<ServiceTypeResponseModel>>
        {
            new List<ServiceTypeResponseModel>(),
            new List<ServiceTypeResponseModel>
            {
                new ServiceTypeResponseModel
                { 
                    Id = Guid.NewGuid(),
                    ServiceTypeName = "Some name",
                    Description = "Some description"
                },
                new ServiceTypeResponseModel
                {
                    Id = Guid.NewGuid()
                },
            },
            new List<ServiceTypeResponseModel>
            {
                new ServiceTypeResponseModel
                {
                    Id = Guid.NewGuid(),
                    RecordStatus = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                },
                new ServiceTypeResponseModel
                {
                    Id = Guid.NewGuid()
                }
            }
        };
    }
}
