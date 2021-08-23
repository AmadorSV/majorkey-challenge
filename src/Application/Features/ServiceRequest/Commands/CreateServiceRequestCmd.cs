using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interface;
using Domain.Enum;


namespace Application.Features.ServiceRequest.Commands
{
    #region Request
    public class CreateServiceRequestCmd : IRequest<Guid>
    {
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    #endregion

    #region Validator

    public class CreateServiceRequestCmdValidator : AbstractValidator<CreateServiceRequestCmd>
    {
        public CreateServiceRequestCmdValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(x => x.BuildingCode)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(x => x.CreatedBy)
                .MaximumLength(100)
                .NotEmpty();
            
            RuleFor(x => x.CreatedDate)
                .NotEmpty();

            RuleFor(x => x.CurrentStatus)
                .IsInEnum();

        }

    }

    #endregion

    #region Handler
    internal class CreateServiceRequestCmdHandler : IRequestHandler<CreateServiceRequestCmd, Guid>
    {
        private readonly IAppContext _context;
        private readonly ILogger<CreateServiceRequestCmdHandler> _logger;

        public CreateServiceRequestCmdHandler(IAppContext context, ILogger<CreateServiceRequestCmdHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateServiceRequestCmd request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Add ServiceRequest received");
                
                var entity = request.Adapt<Domain.Model.ServiceRequest>();

                await _context.ServiceRequests.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);
                
                _logger.LogInformation($"Service request added. Generated Id: {entity.Id}");
                
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed creating Service Request");
                throw;
            }
        }
    }

    #endregion Hanlder
}
