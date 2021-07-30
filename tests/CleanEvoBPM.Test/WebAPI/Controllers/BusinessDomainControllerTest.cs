using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Query;
using CleanEvoBPM.Application.Models.BusinessDomain;
using CleanEvoBPM.Test.WebAPI.Base;
using CleanEvoBPM.WebAPI.Controllers;
using CleanEvoBPM.WebAPI.Helper;
using Moq;
using Xunit;

namespace CleanEvoBPM.Test.WebAPI.Controllers
{
    public class BusinessDomainControllerTest : BaseControllerTest
    {
        private BusinessDomainsController _businessDomainController;
        public BusinessDomainControllerTest() : base(new BusinessDomainsController(null))
        {
            _businessDomainController = (BusinessDomainsController)_apiController;
        }

        [Fact]
        public async Task Get_Success_ReturnIEnumerableBusinessDomainResponseModel()
        {
            var query = new FetchBusinessDomainQuery();
            var responseModel = new List<BusinessDomainResponseModel>
            {
                new BusinessDomainResponseModel 
                {
                    Id= Guid.NewGuid()
                }
            }.AsEnumerable();

            _mockMediator.Setup(x => x.Send(query, new CancellationToken())).Returns(Task.FromResult(responseModel));
            var result = await _businessDomainController.Get(query);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetbyId_Success_ReturnBusinessDomainResponseModel()
        {
            var id = Guid.NewGuid();
            var responseModel = new BusinessDomainResponseModel
            {
                    Id= id
            };

            _mockMediator.Setup(x => x.Send(It.IsAny<GetBusinessDomainDetailsQuery>(), new CancellationToken())).Returns(Task.FromResult(responseModel));
            var result = await _businessDomainController.Get(Guid.NewGuid());
            Assert.Equal(id, result.Value.Id);
        }

        [Fact]
        public async Task Put_Success_ReturnTrue()
        {
            var command = new UpdateBusinessDomainCommand();

            _mockMediator.Setup(x => x.Send(command, new CancellationToken())).Returns(Task.FromResult(true));
            var result = await _businessDomainController.Put(Guid.NewGuid(), command);
            Assert.True(result.Value);
        }

         [Fact]
        public async Task Delete_Success_SuccessGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteBusinessDomainCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = true}));
            var result = await _businessDomainController.Delete(Guid.NewGuid());
            Assert.True(result.Value.Success);
        }
         [Fact]
        public async Task Delete_DeleteMasterDataFailed_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteBusinessDomainCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.DeleteMasterDataFailed}));
            var result = await _businessDomainController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.DeleteMasterDataFailed,result.Value.Message);
        }
         [Fact]
        public async Task Delete_NotFound_FailedGenericResponse()
        {
            _mockMediator.Setup(x => x.Send(It.IsAny<DeleteBusinessDomainCommand>(), new CancellationToken()))
            .Returns(Task.FromResult(new GenericResponse{Success = false, Message = ValidateMessage.NotFound}));
            var result = await _businessDomainController.Delete(Guid.NewGuid());
            Assert.False(result.Value.Success);
            Assert.Equal(ValidateMessage.NotFound,result.Value.Message);
        }
    }
}