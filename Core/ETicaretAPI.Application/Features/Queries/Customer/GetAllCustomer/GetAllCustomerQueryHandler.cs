using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Customer.GetAllCustomer
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQueryRequest, GetAllCustomerQueryResponse> 
    {
        readonly ICustomerReadRepository _customerReadRepository;

        public GetAllCustomerQueryHandler(ICustomerReadRepository customerReadRepository)
        {
            _customerReadRepository = customerReadRepository;
        }
        
        public async Task<GetAllCustomerQueryResponse> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _customerReadRepository.GetAll(false).Count();
            var customers = _customerReadRepository.GetAll(false).Skip(request.Pagination.Page * request.Pagination.Size).Take(request.Pagination.Size).Select(c => new
            {
                c.Id,
                c.Name,
                c.Orders
            }).ToList();

            return new()
            {
                TotalCount = totalCount,
                Customers = customers,
            };
        }
    }
}
