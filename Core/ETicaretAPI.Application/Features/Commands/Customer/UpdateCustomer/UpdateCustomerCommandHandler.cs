using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using C= ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Features.Commands.Customer.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommandResponse>
    {
        readonly private ICustomerReadRepository _customerReadRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;

        public UpdateCustomerCommandHandler(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            C.Customer customer = await _customerReadRepository.GetByIdAsync(request.Id);
            if (customer == null)
            {
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
                return new()
                {
                    Succeeded = true,
                    Message = "Customer updated successfully"
                };
            }

        }
    }
}