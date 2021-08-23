using System;
using Domain.Enum;

namespace Domain.DTO
{
    public record ServiceRequestDTO
    {
        public Guid Id { get; init; }
        public string BuildingCode { get; init; }
        public string Description { get; init; }
        public CurrentStatus CurrentStatus { get; init; }
        public string CreatedBy { get; init; }
        public DateTime CreatedDate { get; init; }
        public string ModifiedBy { get; init; }
        public DateTime? ModifiedDate { get; init; }
    }
}