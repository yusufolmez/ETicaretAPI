using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderWriteRepository orderWriteRepository, ILogger<CreateOrderCommandHandler> logger)
        {
            _orderWriteRepository = orderWriteRepository;
            _logger = logger;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                CustomerId = Guid.Parse(request.CustomerId),
                Description = request.Description,
                Address = request.Address
            });
            await _orderWriteRepository.SaveAsync();
            _logger.LogInformation($"Sipariş oluşturuldu, Müşteri ID: {request.CustomerId}");
            return new()
            {
                Succeeded = true,
                Message = "Order created successfully.",
            };
        }
    }
}
