using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using C = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Features.Commands.Customer.RemoveCustomer
{
    public class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommandRequest, RemoveCustomerCommandResponse>
    {
        readonly private ICustomerReadRepository _customerReadRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;

        public RemoveCustomerCommandHandler(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        public async Task<RemoveCustomerCommandResponse> Handle(RemoveCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            C.Customer customer = await _customerReadRepository.GetByIdAsync(request.Id, false);
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
                _customerWriteRepository.Remove(customer);
                await _customerWriteRepository.SaveAsync();
                return new()
                {
                    Succeeded = true,
                    Message = "Customer removed successfully"
                };
            }
        }
    }
}
