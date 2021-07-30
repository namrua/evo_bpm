using System;
using System.Collections.Generic;
using System.Text;
using CleanEvoBPM.Application.CQRS.Project.CommandHandler;
using CleanEvoBPM.Application.Models.ProjectType;
using Xunit;

namespace CleanEvoBPM.Test.Application.CQRS.Project.CommandHandler
{
    public class CheckStatusTest
    {
        public CheckStatusTest() 
        { 
        }

        [Fact]
        public void CheckStatus_Success_ReturnTrue()
        {
            //Setup
            var projectType = new ProjectTypeResponseModel
            {
                Id = Guid.NewGuid(),
                RecordStatus = true
            };

            //Run Service
            var result = CheckStatus<ProjectTypeResponseModel>.Checking(projectType, "RecordStatus");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckStatus_Failed_ReturnFalse()
        {
            //Setup
            var projectType = new ProjectTypeResponseModel
            {
                Id = Guid.NewGuid(),
                RecordStatus = false
            };

            //Run Service
            var result = CheckStatus<ProjectTypeResponseModel>.Checking(projectType, "RecordStatus");

            //Assert
            Assert.False(result);
        }
    }
}
