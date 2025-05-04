using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using C= ETicaretAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.Customer.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommandResponse>
    {
        readonly private ICustomerReadRepository _customerReadRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;
        readonly ILogger<UpdateCustomerCommandHandler> _logger;

        public UpdateCustomerCommandHandler(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository, ILogger<UpdateCustomerCommandHandler> logger)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
            _logger = logger;
        }

        public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            C.Customer customer = await _customerReadRepository.GetByIdAsync(request.Id);
            if (customer == null)
            {
                _logger.LogError($"Müşteri bulunamadı, ID: {request.Id}");
                return new()
                {
                    Succeeded = false,
                    Message = "Customer not found"
                };
            }
            else
            {
                customer.Name = request.Name;
                await _customerWriteRepository.SaveAsync();
                _logger.LogInformation($"Müşteri güncellendi, ID: {request.Id}, Yeni İsim: {request.Name}");
                return new()
                {
                    Succeeded = true,
                    Message = "Customer updated successfully"
                };
            }
        }
    }
}