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
using ETicaretAPI.Application.Abstractions.Services;

namespace ETicaretAPI.Application.Features.Queries.Order.GetAllOrder
{
    public class GetAllOrdersQueryhandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetAllOrdersQueryhandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _orderService.GetAllOrdersAsync(request.Pagination.Page, request.Pagination.Size);
            return new()
            {
                TotalOrderCount = data.TotalOrderCount,
                Orders = data.Orders
            };
        }
    }
}
