using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using System;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.ServiceType
{
    public partial class ServiceTypeTest
    {
        public static TheoryData<CreateServiceTypeCommand> CreateServiceTypeDataValid
        {
            get
            {
                var data = new TheoryData<CreateServiceTypeCommand>
                {
                    new CreateServiceTypeCommand
                    {
                        ServiceTypeName = "Service 01",
                        Description = "Service 01 description",
                        RecordStatus = true
                    },
                    new CreateServiceTypeCommand
                    {
                        ServiceTypeName = "Rapid app development",
                        Description = "RAD description",
                    }
                };

                return data;
            }
        }

        public static TheoryData<UpdateServiceTypeCommand> UpdateServiceTypeDataValid
        {
            get
            {
                var data = new TheoryData<UpdateServiceTypeCommand>
                {
                    new UpdateServiceTypeCommand
                    {
                        Id = new Guid("EB69F33B-FF49-409B-A4F2-011AC0E809A8"),
                        ServiceTypeName = "Service 01",
                        Description = "Service 01 description",
                        RecordStatus = true
                    },
                    new UpdateServiceTypeCommand
                    {
                        Id = new Guid("274451E4-6057-45CF-AC38-1AAE77AA0A66"),
                        ServiceTypeName = "Rapid app development",
                        Description = "RAD description",
                    }
                };

                return data;
            }
        }

        public static TheoryData<DeleteServiceTypeCommand> DeleteServiceTypeDataValid
        {
            get
            {
                var data = new TheoryData<DeleteServiceTypeCommand>
                {
                    new DeleteServiceTypeCommand { Id = Guid.NewGuid() },
                };

                return data;
            }
        }

        public static TheoryData<GenericResponse> CreateServiceTypeResult
        {
            get
            {
                var data = new TheoryData<GenericResponse>
                {
                    new GenericResponse(),
                    new GenericResponse
                    {
                        Success = false
                    },
                    GenericResponse.FailureResult()
                };

                return data;
            }
        }
    }
}
