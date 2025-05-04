using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using O = ETicaretAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Queries.Order.GetByIdOrder
{
    public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQueryRequest, GetByIdOrderQueryResponse>
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly ILogger<GetByIdOrderQueryHandler> _logger;

        public GetByIdOrderQueryHandler(IOrderReadRepository orderReadRepository, ILogger<GetByIdOrderQueryHandler> logger)
        {
            _orderReadRepository = orderReadRepository;
            _logger = logger;
        }

        public async Task<GetByIdOrderQueryResponse> Handle(GetByIdOrderQueryRequest request, CancellationToken cancellationToken)
        {
            O.Order order = await _orderReadRepository.GetByIdAsync(request.Id, false);
            _logger.LogInformation($"Sipariş bilgileri alındı, ID: {request.Id}");
            return new()
            {
                CustomerId = order.CustomerId,
                Address = order.Address,
                Description = order.Description
            };
        }
    }
}
