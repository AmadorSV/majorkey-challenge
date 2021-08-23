using FluentValidation;
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
using Domain.Enum;

namespace Application.Features.ServiceRequest.Commands
{
    #region Request
    public record UpdateServiceRequestCmd : IRequest<Unit>
    {
        public Guid Id { get; init; }
        public string BuildingCode { get; init; }
        public string Description { get; init; }
        public CurrentStatus CurrentStatus { get; init; }
        public string ModifiedBy { get; set; }
    }

    #endregion

    #region Validator

    public class UpdateServiceRequestCmdValidator : AbstractValidator<UpdateServiceRequestCmd>
    {

        public UpdateServiceRequestCmdValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(200);

            RuleFor(x => x.BuildingCode)
                .MaximumLength(100);

            RuleFor(x => x.ModifiedBy)
                .NotEmpty();
        }
    }

    #endregion

    #region Handler
    internal class UpdateServiceRequestCmdHandler : IRequestHandler<UpdateServiceRequestCmd, Unit>
    {
        private readonly IAppContext _context;
        private readonly ILogger<UpdateServiceRequestCmdHandler> _logger;

        public UpdateServiceRequestCmdHandler(IAppContext context, ILogger<UpdateServiceRequestCmdHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateServiceRequestCmd request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.ServiceRequests.FindAsync(request.Id);

                if (entity == null) 
                    throw new NotFoundException("Service request to update not found");

                request.Adapt(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed updating service request");
                throw;
            }
            
        }

    }

    #endregion Hanlder
}
