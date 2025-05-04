using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using O = ETicaretAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.Order.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest, DeleteOrderCommandResponse>
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _logger = logger;
        }

        public Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            O.Order order = _orderReadRepository.GetByIdAsync(request.Id).Result;
            if (order == null)
            {
                _logger.LogError($"Sipariş bulunamadı, ID: {request.Id}");
                return Task.FromResult(new DeleteOrderCommandResponse()
                {
                    Succeeded = false,
                    Message = "Order not found."
                });
            }
            _orderWriteRepository.Remove(order);
            _orderWriteRepository.SaveAsync();
            _logger.LogInformation($"Sipariş silindi, ID: {request.Id}");
            return Task.FromResult(new DeleteOrderCommandResponse()
            {
                Succeeded = true,
                Message = "Order deleted successfully."
            });
        }
    }
}
