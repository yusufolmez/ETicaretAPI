using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using O = ETicaretAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.Order.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommandRequest, UpdateOrderCommandResponse>
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _logger = logger;
        }

        public async Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommandRequest updateOrderCommandRequest, CancellationToken cancellationToken)
        {
            O.Order order = await _orderReadRepository.GetByIdAsync(updateOrderCommandRequest.Id);
            if (order == null)
            {
                _logger.LogError($"Sipariş bulunamadı, ID: {updateOrderCommandRequest.Id}");
                return new() {Succeeded = false, Message = "Order not found.",};
            }
            order.Description = updateOrderCommandRequest.Description;
            order.Address = updateOrderCommandRequest.Address;
            await _orderWriteRepository.SaveAsync();
            _logger.LogInformation($"Sipariş güncellendi, ID: {updateOrderCommandRequest.Id}");
            return new()
            {
                Succeeded = true,
                Message = "Order updated successfully.",
            };
        }
    }
}