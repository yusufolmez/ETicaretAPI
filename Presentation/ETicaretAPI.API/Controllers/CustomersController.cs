using System.Net;
using ETicaretAPI.Application.Features.Commands.Customer.CreateCustomer;
using ETicaretAPI.Application.Features.Commands.Customer.RemoveCustomer;
using ETicaretAPI.Application.Features.Commands.Customer.UpdateCustomer;
using ETicaretAPI.Application.Features.Queries.Customer.GetAllCustomer;
using ETicaretAPI.Application.Features.Queries.Customer.GetByIdCustomer;
using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        readonly private ICustomerReadRepository _customerReadRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;
        readonly IMediator _mediator;

        public CustomersController(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository, IMediator mediator)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCustomerQueryRequest getAllCustomerQueryRequest)
        {
            GetAllCustomerQueryResponse response = await _mediator.Send(getAllCustomerQueryRequest);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdCustomerQueryRequest getByIdCustomerQueryRequest)
        {
            GetByIdCustomerQueryResponse response = await _mediator.Send(getByIdCustomerQueryRequest);
            if (response == null)
                return NotFound();
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommandRequest createCustomerCommandRequest)
        {
            CreateCustomerCommandResponse response = await _mediator.Send(createCustomerCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCustomerCommandRequest updateCustomerCommandRequest)
        {
            UpdateCustomerCommandResponse response = await _mediator.Send(updateCustomerCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveCustomerCommandRequest removeCustomerCommandRequest)
        {
            RemoveCustomerCommandResponse response = await _mediator.Send(removeCustomerCommandRequest);
            return Ok(response);
        }
    }
}
