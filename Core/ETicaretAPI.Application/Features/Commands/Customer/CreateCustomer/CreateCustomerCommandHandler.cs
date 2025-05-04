using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.Customer.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommandRequest, CreateCustomerCommandResponse>
    {
        readonly ICustomerWriteRepository _customerWriteRepository;
        readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository, ILogger<CreateCustomerCommandHandler> logger)
        {
            _customerWriteRepository = customerWriteRepository;
            _logger = logger;
        }
        public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            await _customerWriteRepository.AddAsync(new()
            {
                Name = request.Name
            });
            await _customerWriteRepository.SaveAsync();
            _logger.LogInformation($"Müşteri oluşturuldu, İsim: {request.Name}");
            return new();
        }
    }
}
