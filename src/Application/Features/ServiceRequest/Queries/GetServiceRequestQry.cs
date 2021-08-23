using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exception;
using Application.Common.Interface;
using Domain.DTO;

namespace Application.Features.ServiceRequest.Queries
{
    public class GetServiceRequestQry : IRequest<ServiceRequestDTO>
    {
        public Guid Id { get; set; }

        public GetServiceRequestQry(Guid id)
        {
            Id = id;
        }
    }

    #region Handler
    internal class GetPermissionCmdHandler : IRequestHandler<GetServiceRequestQry, ServiceRequestDTO>
    {
        private readonly IAppContext _context;
        private readonly ILogger<GetPermissionCmdHandler> _logger;

        public GetPermissionCmdHandler(IAppContext context, ILogger<GetPermissionCmdHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceRequestDTO> Handle(GetServiceRequestQry request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get ServiceRequest received");

            var entity = await _context.ServiceRequests
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id.Equals(request.Id));

            if (entity == null)
                throw new NotFoundException("ServiceRequest not found");
            
            _logger.LogInformation("Returning ServiceRequest");

            return entity.Adapt<ServiceRequestDTO>();
        }

    }

    #endregion Hanlder
}
