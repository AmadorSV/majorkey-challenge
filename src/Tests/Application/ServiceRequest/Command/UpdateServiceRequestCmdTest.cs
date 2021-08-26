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
    
    public class UpdateServiceRequestCmdTest
    {
        [Test]
        public async Task UpdateRequestt_WithFilledCmd_ShouldUpdate()
        {
            // Arrage

            var updateCommand = new UpdateServiceRequestCmd
            {
                Description = "New Any Description",
                BuildingCode = "new123456764532",
                CurrentStatus = CurrentStatus.Canceled,
                ModifiedBy = "NewUser"
            };
            

            // Act

            var createdGuid = await CreateServiceRequest();
            var currentDate = DateTime.Now;
            
            updateCommand.Id = createdGuid;
            await SendAsync(updateCommand);
            var updatedEntity = await FindAsync<Domain.Model.ServiceRequest>(createdGuid);
            
            // Assert

            updatedEntity.Should().NotBeNull();
            updatedEntity.Description.Should().Be(updateCommand.Description);
            updatedEntity.BuildingCode.Should().Be(updateCommand.BuildingCode);
            updatedEntity.ModifiedBy.Should().Be(updateCommand.ModifiedBy);
            updatedEntity.ModifiedDate.Should().BeCloseTo(currentDate,TimeSpan.FromSeconds(2));

        }
        
        [Test]
        public void UpdateRequest_WithNotExistingId_ReturnsNotFound()
        {
            // Arrage
            var updateCommand = new UpdateServiceRequestCmd
            {
                Id = new Guid()
            };
            
            // Assert

            Assert.ThrowsAsync<NotFoundException>(async () => await SendAsync(updateCommand));
        }
       
    }
};