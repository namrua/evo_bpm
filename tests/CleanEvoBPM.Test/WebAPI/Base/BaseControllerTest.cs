using CleanEvoBPM.WebAPI.Controllers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace CleanEvoBPM.Test.WebAPI.Base
{
    public abstract class BaseControllerTest
    {
        protected Mock<IMediator> _mockMediator;
        protected CustomBaseApiController _apiController;
        public BaseControllerTest(CustomBaseApiController apiController)
        {
            _mockMediator = new Mock<IMediator>();
            _apiController = apiController;

            var services = new ServiceCollection();
            services.AddScoped<IMediator>(p => _mockMediator.Object);
            _apiController.ControllerContext.HttpContext = new DefaultHttpContext()
            {
                RequestServices = services.BuildServiceProvider()
            };
            
        }             
    }
}
