using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Methodology.Command;
using System;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.Methodology
{
    public partial class MethodologyTest
    {
        public static TheoryData<CreateMethodologyCommand> CreateMethodologyDataValid
        {
            get
            {
                var data = new TheoryData<CreateMethodologyCommand>
                {
                    new CreateMethodologyCommand
                    {
                        MethodologyName = "Agile",
                        Description = "Agile description",
                        RecordStatus = true
                    },
                    new CreateMethodologyCommand
                    {
                        MethodologyName = "Rapid app development",
                        Description = "RAD description",
                    }
                };

                return data;
            }
        }

        public static TheoryData<UpdateMethodologyCommand> UpdateMethodologyDataValid
        {
            get
            {
                var data = new TheoryData<UpdateMethodologyCommand>
                {
                    new UpdateMethodologyCommand
                    {
                        Id = new Guid("EB69F33B-FF49-409B-A4F2-011AC0E809A8"),
                        MethodologyName = "Agile",
                        Description = "Agile description",
                        RecordStatus = true
                    },
                    new UpdateMethodologyCommand
                    {
                        Id = new Guid("274451E4-6057-45CF-AC38-1AAE77AA0A66"),
                        MethodologyName = "Rapid app development",
                        Description = "RAD description",
                    }
                };

                return data;
            }
        }

        public static TheoryData<DeleteMethodologyCommand> DeleteMethodologyDataValid
        {
            get
            {
                var data = new TheoryData<DeleteMethodologyCommand>
                {
                    new DeleteMethodologyCommand { Id = Guid.NewGuid() },
                };

                return data;
            }
        }

        public static TheoryData<GenericResponse> CreateMethodologyResult
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
