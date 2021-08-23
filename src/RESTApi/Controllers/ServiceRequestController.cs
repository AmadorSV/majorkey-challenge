using System;
using System.Threading.Tasks;
using Application.Common.Exception;
using Application.Features.ServiceRequest.Commands;
using Application.Features.ServiceRequest.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTApi.Controllers
{
    [Route("api/[controller]")]
    public class ServiceRequestController: ControllerBase
    {
        readonly IMediator _mediator;

        public ServiceRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllServiceRequests()
        {
            var result = await _mediator.Send(new GetAllServiceRequestsQry());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceRequests(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetServiceRequestQry(id));
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> AddServiceRequest([FromBody] CreateServiceRequestCmd request)
        {
            var result = await _mediator.Send(request);
            return Created("null",result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateServiceRequest([FromBody] UpdateServiceRequestCmd request)
        {
            try
            {
                var result = await _mediator.Send(request);

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRequest([FromRoute] Guid id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteServiceRequestCmd(id));

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}