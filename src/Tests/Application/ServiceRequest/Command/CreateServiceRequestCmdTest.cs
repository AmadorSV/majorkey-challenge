using System;
using System.Threading.Tasks;
using Application.Common.Exception;
using Application.Features.ServiceRequest.Commands;
using Domain.Enum;
using NUnit.Framework;
using FluentAssertions;

namespace Tests.Application.ServiceRequest
{
    using static Testing;
    using static Shared;
    
    public class CreateServiceRequestCmdTest
    {
        [Test]
        public async Task CreateServiceRequest_FilledCmd_ShouldCreate()
        {
    
            // Arrange
            
            var command =  new CreateServiceRequestCmd
            {
                Description = "Any Description",
                BuildingCode = "123456764532",
                CurrentStatus = CurrentStatus.InProgress,
                CreatedBy = "User",
                CreatedDate = DateTime.Now
            };
            
            // Act
            
            var createdGuid = await CreateServiceRequest(command);
            var createdItem = await FindAsync<Domain.Model.ServiceRequest>(createdGuid);

            // Assert

            createdItem.Should().NotBeNull();
            createdItem.Description.Should().Be(command.Description);
            createdItem.BuildingCode.Should().Be(command.BuildingCode);
            createdItem.CreatedBy.Should().Be(command.CreatedBy);
            createdItem.CreatedDate.Should().BeCloseTo(command.CreatedDate.Value,TimeSpan.FromSeconds(1));
            createdItem.ModifiedBy.Should().Be(null);
            createdItem.ModifiedDate.Should().Be(null);

        }
    }
};