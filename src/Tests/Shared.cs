using System;
using System.Threading.Tasks;
using Application.Features.ServiceRequest.Commands;
using Domain.Enum;

namespace Tests
{
    using static Testing;
    public class Shared
    {
        public static async Task<Guid> CreateServiceRequest(CreateServiceRequestCmd command = null)
        {
            // Arrage
            command ??= new CreateServiceRequestCmd
            {
                Description = "Any Description",
                BuildingCode = "123456764532",
                CurrentStatus = CurrentStatus.InProgress,
                CreatedBy = "User",
                CreatedDate = DateTime.Now
            };

            // Act

            return await SendAsync(command);
        }
    }
}