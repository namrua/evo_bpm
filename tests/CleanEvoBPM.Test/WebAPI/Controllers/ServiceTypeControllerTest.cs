using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.ServiceType.Query;
using CleanEvoBPM.Application.CQRS.ServiceType.Command;
using CleanEvoBPM.Application.Models.ServiceType;
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
    public class ServiceTypeControllerTest : BaseControllerTest
    {
        private ServiceTypeController _serviceTypeController;

        public ServiceTypeControllerTest() : base(new ServiceTypeController(null))
        {
            _serviceTypeController = (ServiceTypeController)_apiController;
        }

        [Theory]
        [MemberData(nameof(GetServiceTypeData))]
        public async Task GetServiceType_Success_DataReturnsNull(GetServiceTypeQuery query)
        {
            _mockMediator
                .Setup(x => x.Send(It.IsAny<GetServiceTypeQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(null as IEnumerable<ServiceTypeResponseModel>));

            var result = await _serviceTypeController.Get(query);

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(GetServiceTypeData))]

        public async Task GetServiceType_Success_DataReturnsNotNullList(GetServiceTypeQuery query)
        {
            var returnedData = new List<ServiceTypeResponseModel>
            {
                new ServiceTypeResponseModel { Id = Guid.NewGuid() },
            };

            _mockMediator
                .Setup(x => x.Send(It.IsAny<GetServiceTypeQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(returnedData as IEnumerable<ServiceTypeResponseModel>));

            var result = await _serviceTypeController.Get(query);

            Assert.Equal(returnedData.Count, result.Count());
        }

        [Theory]
        [MemberData(nameof(CreateServiceTypeDataValid))]
        public async Task CreateServiceType_Success(CreateServiceTypeCommand command)
        {
            var responseStubbed = GenericResponse.SuccessResult();

            _mockMediator
                .Setup(x => x.Send(It.IsAny<CreateServiceTypeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(responseStubbed));

            var result = await _serviceTypeController.Post(command);

            Assert.True(result.Value.Success);
        }

        [Theory]
        [MemberData(nameof(CreateServiceTypeDataValid))]
        public async Task CreateServiceType_Failure(CreateServiceTypeCommand command)
        {
            var responseStubbed = GenericResponse.FailureResult();

            _mockMediator
                .Setup(x => x.Send(It.IsAny<CreateServiceTypeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(responseStubbed));

            var result = await _serviceTypeController.Post(command);

            Assert.False(result.Value.Success);
        }

        [Theory]
        [MemberData(nameof(UpdateServiceTypeDataValid))]
        public async Task UpdateServiceType_Success(UpdateServiceTypeCommand command)
        {
            _mockMediator
                .Setup(x => x.Send(It.IsAny<UpdateServiceTypeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            var result = await _serviceTypeController.Put(command.Id, command);

            Assert.True(result.Value);
        }

        [Theory]
        [MemberData(nameof(UpdateServiceTypeDataValid))]
        public async Task UpdateServiceType_Failure(UpdateServiceTypeCommand command)
        {
            _mockMediator
                .Setup(x => x.Send(It.IsAny<UpdateServiceTypeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(false));

            var result = await _serviceTypeController.Put(command.Id, command);

            Assert.False(result.Value);
        }


        [Fact]
        public async Task Delete_Success_SuccessGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteServiceTypeCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = true}));
            var result = await _serviceTypeController.Delete(Guid.NewGuid());
            Assert.True(result.Value.Success);
        }
         [Fact]
        public async Task Delete_DeleteMasterDataFailed_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteServiceTypeCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.DeleteMasterDataFailed}));
            var result = await _serviceTypeController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed,result.Value.Message);
        }
         [Fact]
        public async Task Delete_NotFound_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteServiceTypeCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.NotFound}));
            var result = await _serviceTypeController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.NotFound,result.Value.Message);
        }

        public static TheoryData<List<ServiceTypeResponseModel>> GetResultMocked = new TheoryData<List<ServiceTypeResponseModel>>
        {
            new List<ServiceTypeResponseModel>(),
            new List<ServiceTypeResponseModel>
            {
                new ServiceTypeResponseModel { Id = Guid.NewGuid() },
            },
            new List<ServiceTypeResponseModel>
            {
                new ServiceTypeResponseModel { Id = Guid.NewGuid() },
                new ServiceTypeResponseModel { Id = Guid.NewGuid() }
            }
        };

        public static TheoryData<GetServiceTypeQuery> GetServiceTypeData
        {
            get
            {
                var data = new TheoryData<GetServiceTypeQuery>
                {
                    new GetServiceTypeQuery(),
                    new GetServiceTypeQuery
                    {
                        RecordStatus = true
                    }
                };

                return data;
            }
        }

        public static TheoryData<CreateServiceTypeCommand> CreateServiceTypeDataValid
        {
            get
            {
                var data = new TheoryData<CreateServiceTypeCommand>
                {
                    new CreateServiceTypeCommand
                    {
                        ServiceTypeName = "Agile",
                        Description = "Agile description",
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

        public static TheoryData<UpdateServiceTypeCommand> UpdateServiceTypeDataValid
        {
            get
            {
                var data = new TheoryData<UpdateServiceTypeCommand>
                {
                    new UpdateServiceTypeCommand
                    {
                        Id = new Guid("EB69F33B-FF49-409B-A4F2-011AC0E809A8"),
                        ServiceTypeName = "Agile",
                        Description = "Agile description",
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

        public static TheoryData<Guid> DeleteServiceTypeDataValid
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
