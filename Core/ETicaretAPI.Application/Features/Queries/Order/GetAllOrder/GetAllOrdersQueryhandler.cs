using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using O = ETicaretAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Queries.Order.GetAllOrder
{
    public class GetAllOrdersQueryhandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly ILogger<GetAllOrdersQueryhandler> _logger;
        public GetAllOrdersQueryhandler(IOrderReadRepository orderReadRepository, ILogger<GetAllOrdersQueryhandler> logger)
        {
            _orderReadRepository = orderReadRepository;
            _logger = logger;
        }
        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _orderReadRepository.GetAll(false).Count();
            var orders = _orderReadRepository.GetAll(false).Skip(request.Pagination.Page * request.Pagination.Size).Take(request.Pagination.Size).Select(o => new
            {
                o.Id,
                o.Description,
                o.Products,
                o.CustomerId

            }).ToList();

            _logger.LogInformation($"Tüm siparişler listelendi, Toplam Sayı: {totalCount}");
            return new()
            {
                TotalCount = 0,
                Orders = orders
            };
        }
    }
}
