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
using Application.Common.Interface;
using Domain.DTO;

namespace Application.Features.ServiceRequest.Queries
{
    public class GetAllServiceRequestsQry : IRequest<IReadOnlyList<ServiceRequestDTO>>
    {
    }

    #region Handler
    internal class GetAllServiceRequestCmdHandler : IRequestHandler<GetAllServiceRequestsQry, IReadOnlyList<ServiceRequestDTO>>
    {
        private readonly IAppContext _context;
        private readonly ILogger<GetAllServiceRequestCmdHandler> _logger;

        public GetAllServiceRequestCmdHandler(IAppContext context, ILogger<GetAllServiceRequestCmdHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IReadOnlyList<ServiceRequestDTO>> Handle(GetAllServiceRequestsQry request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get All ServiceRequest received");

            var entities = await _context.ServiceRequests
                .AsNoTracking()
                .ToListAsync();
            
            _logger.LogInformation("Returning ServiceRequests");
            
            return entities.Count==0? null: entities.Adapt<IReadOnlyList<ServiceRequestDTO>>();
        }

    }

    #endregion Hanlder
}
