using System;
using System.Threading.Tasks;
using Application.Common.Exception;
using Application.Features.ServiceRequest.Commands;
using Application.Features.ServiceRequest.Queries;
using Domain.Enum;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Application.ServiceRequest.Query
{
    using static Testing;
    using static Shared;
    
    public class GetServiceRequestTest
    {
        [Test]
        public async Task GetServiceRequest_NoExistingId_NotFound()
        {
            // Arrage
            var qry = new GetServiceRequestQry(new Guid());

            // Act
            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await SendAsync(qry));
        }
        
        [Test]
        public async Task GetServiceRequest_WithExistingId_NotNull()
        {
            // Arrage
            var id = await CreateServiceRequest();
            var qry = new GetServiceRequestQry(id);

            // Act
            var result = await SendAsync(qry);
            // Assert
            
            result.Should().NotBeNull();
        }
    }
}