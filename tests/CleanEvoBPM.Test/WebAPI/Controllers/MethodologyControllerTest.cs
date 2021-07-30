using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.Methodology.Command;
using CleanEvoBPM.Application.CQRS.Methodology.Query;
using CleanEvoBPM.Application.Models.Methodology;
using CleanEvoBPM.Test.WebAPI.Base;
using CleanEvoBPM.WebAPI.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanEvoBPM.Test.WebAPI.Controllers
{
    public class MethodologyControllerTest : BaseControllerTest
    {
        private MethodologyController _methodologyController;

        public MethodologyControllerTest() : base(new MethodologyController(null))
        {
            _methodologyController = (MethodologyController)_apiController;
        }

        [Theory]
        [MemberData(nameof(GetMethodologyData))]
        public async Task GetMethodology_Success_DataReturnsNull(GetMethodologyQuery query)
        {
            _mockMediator
                .Setup(x => x.Send(It.IsAny<GetMethodologyQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(null as IEnumerable<MethodologyResponseModel>));

            var result = await _methodologyController.Get(query);

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(GetMethodologyData))]
        
        public async Task GetMethodology_Success_DataReturnsNotNullList(GetMethodologyQuery query)
        {
            var returnedData = new List<MethodologyResponseModel>
            {
                new MethodologyResponseModel { Id = Guid.NewGuid() },
            };

            _mockMediator
                .Setup(x => x.Send(It.IsAny<GetMethodologyQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(returnedData as IEnumerable<MethodologyResponseModel>));

            var result = await _methodologyController.Get(query);

            Assert.Equal(returnedData.Count, result.Count());
        }

        [Theory]
        [MemberData(nameof(CreateMethodologyDataValid))]
        public async Task CreateMethodology_Success(CreateMethodologyCommand command)
        {
            var responseStubbed = GenericResponse.SuccessResult();

            _mockMediator
                .Setup(x => x.Send(It.IsAny<CreateMethodologyCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(responseStubbed));

            var result = await _methodologyController.Post(command);

            Assert.True(result.Value.Success);
        }

        [Theory]
        [MemberData(nameof(CreateMethodologyDataValid))]
        public async Task CreateMethodology_Failure(CreateMethodologyCommand command)
        {
            var responseStubbed = GenericResponse.FailureResult();

            _mockMediator
                .Setup(x => x.Send(It.IsAny<CreateMethodologyCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(responseStubbed));

            var result = await _methodologyController.Post(command);

            Assert.False(result.Value.Success);
        }

        [Theory]
        [MemberData(nameof(UpdateMethodologyDataValid))]
        public async Task UpdateMethodology_Success(UpdateMethodologyCommand command)
        {
            _mockMediator
                .Setup(x => x.Send(It.IsAny<UpdateMethodologyCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var result = await _methodologyController.Put(command.Id, command);

            Assert.True(result.Value);
        }

        [Theory]
        [MemberData(nameof(UpdateMethodologyDataValid))]
        public async Task UpdateMethodology_Failure(UpdateMethodologyCommand command)
        {
            _mockMediator
                .Setup(x => x.Send(It.IsAny<UpdateMethodologyCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));

            var result = await _methodologyController.Put(command.Id, command);

            Assert.False(result.Value);
        }

        [Fact]
        public async Task Delete_Success_SuccessGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteMethodologyCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = true}));
            var result = await _methodologyController.Delete(Guid.NewGuid());
            Assert.True(result.Value.Success);
        }
         [Fact]
        public async Task Delete_DeleteMasterDataFailed_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteMethodologyCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.DeleteMasterDataFailed}));
            var result = await _methodologyController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed,result.Value.Message);
        }
         [Fact]
        public async Task Delete_NotFound_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteMethodologyCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.NotFound}));
            var result = await _methodologyController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.NotFound,result.Value.Message);
        }

        public static TheoryData<List<MethodologyResponseModel>> GetResultMocked = new TheoryData<List<MethodologyResponseModel>>
        {
            new List<MethodologyResponseModel>(),
            new List<MethodologyResponseModel>
            {
                new MethodologyResponseModel { Id = Guid.NewGuid() },
            },
            new List<MethodologyResponseModel>
            {
                new MethodologyResponseModel { Id = Guid.NewGuid() },
                new MethodologyResponseModel { Id = Guid.NewGuid() }
            }
        };

        public static TheoryData<GetMethodologyQuery> GetMethodologyData
        {
            get
            {
                var data = new TheoryData<GetMethodologyQuery>
                {
                    new GetMethodologyQuery(),
                    new GetMethodologyQuery
                    {
                        RecordStatus = true
                    }
                };

                return data;
            }
        }

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

        public static TheoryData<Guid> DeleteMethodologyDataValid
        {
            get
            {
                var data = new TheoryData<Guid>
                {
                    new Guid("EB69F33B-FF49-409B-A4F2-011AC0E809A8"),
                    new Guid("274451E4-6057-45CF-AC38-1AAE77AA0A66"),
                };

                return data;
            }
        }
    }
}
