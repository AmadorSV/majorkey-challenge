﻿using Application.Common.Interface;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exception;

namespace Application.Features.ServiceRequest.Commands
{
    #region Request
    public record DeleteServiceRequestCmd : IRequest<Unit>
    {
        public int Id { get; init; }

        public DeleteServiceRequestCmd(int id)
        {
            Id = id;
        }
    }

    #endregion

    #region Handler
    internal class DeletePermissionCmdHandler : IRequestHandler<DeleteServiceRequestCmd, Unit>
    {
        private readonly IAppContext _context;
        private readonly ILogger<DeletePermissionCmdHandler> _logger;

        public DeletePermissionCmdHandler(IAppContext context, ILogger<DeletePermissionCmdHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteServiceRequestCmd request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.ServiceRequests.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException("Service Request to delete not found.");
                }

                _context.ServiceRequests.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed deleting permission");
                throw;
            }

        }

    }

    #endregion Hanlder
}
