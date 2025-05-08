using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Queries.Customer.GetAllCustomer
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQueryRequest, GetAllCustomerQueryResponse> 
    {
        readonly ICustomerReadRepository _customerReadRepository;
        readonly ILogger<GetAllCustomerQueryHandler> _logger;

        public GetAllCustomerQueryHandler(ICustomerReadRepository customerReadRepository, ILogger<GetAllCustomerQueryHandler> logger)
        {
            _customerReadRepository = customerReadRepository;
            _logger = logger;
        }
        
        public async Task<GetAllCustomerQueryResponse> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _customerReadRepository.GetAll(false).Count();
            var customers = _customerReadRepository.GetAll(false).Skip(request.Pagination.Page * request.Pagination.Size).Take(request.Pagination.Size).Select(c => new
            {
                c.Id,
                c.Name,
                //c.Orders
            }).ToList();

            _logger.LogInformation($"Tüm müşteriler listelendi, Toplam Sayı: {totalCount}");
            return new()
            {
                TotalCount = totalCount,
                Customers = customers,
            };
        }
    }
}
