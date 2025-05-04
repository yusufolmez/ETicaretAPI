using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using C= ETicaretAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Queries.Customer.GetByIdCustomer
{
    public class GetByIdCustomerQueryHandler : IRequestHandler<GetByIdCustomerQueryRequest, GetByIdCustomerQueryResponse>
    {
        readonly ICustomerReadRepository _customerReadRepository;
        readonly ILogger<GetByIdCustomerQueryHandler> _logger;

        public GetByIdCustomerQueryHandler(ICustomerReadRepository customerReadRepository, ILogger<GetByIdCustomerQueryHandler> logger)
        {
            _customerReadRepository = customerReadRepository;
            _logger = logger;
        }

        public async Task<GetByIdCustomerQueryResponse> Handle(GetByIdCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            C.Customer customer = await _customerReadRepository.GetByIdAsync(request.Id, false);
            _logger.LogInformation($"Müşteri bilgileri alındı, ID: {request.Id}");
            return new()
            {
                Name = customer.Name,
            };
        }
    }
}
