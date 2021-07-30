using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.Models.Methodology;
using System;
using System.Collections.Generic;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.Methodology
{
    public partial class MethodologyTest
    {
        public static TheoryData<GetMethodologyQuery> GetMethodologyData
        {
            get
            {
                var data = new TheoryData<GetMethodologyQuery>
                {
                    new GetMethodologyQuery(),
                    new GetMethodologyQuery
                    {
                        RecordStatus = true,
                        Id = Guid.NewGuid(),
                        Search = "Some text"
                    }
                };

                return data;
            }
        }

        public static TheoryData<List<MethodologyResponseModel>> GetResultMocked = new TheoryData<List<MethodologyResponseModel>>
        {
            new List<MethodologyResponseModel>(),
            new List<MethodologyResponseModel>
            {
                new MethodologyResponseModel {
                    Id = Guid.NewGuid(),
                    MethodologyName = "Some name",
                    Description = "Some description"
                },
            },
            new List<MethodologyResponseModel>
            {
                new MethodologyResponseModel
                {
                    Id = Guid.NewGuid(),
                    RecordStatus = true,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                },
                new MethodologyResponseModel
                {
                    Id = Guid.NewGuid()
                }
            }
        };
    }
}
