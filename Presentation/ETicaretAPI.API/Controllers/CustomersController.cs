using System.Net;
using ETicaretAPI.Application.Features.Commands.Customer.CreateCustomer;
using ETicaretAPI.Application.Features.Queries.Customer.GetAllCustomer;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var customer = await _customerReadRepository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommandRequest createCustomerCommandRequest)
        {
            CreateCustomerCommandResponse response = await _mediator.Send(createCustomerCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
