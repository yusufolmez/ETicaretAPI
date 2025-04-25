using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using O = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Features.Commands.Order.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest, DeleteOrderCommandResponse>
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly IOrderWriteRepository _orderWriteRepository;

        public DeleteOrderCommandHandler(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            O.Order order = _orderReadRepository.GetByIdAsync(request.Id).Result;
            if (order == null)
                return Task.FromResult(new DeleteOrderCommandResponse()
                {
                    Succeeded = false,
                    Message = "Order not found."
                });
            _orderWriteRepository.Remove(order);
            _orderWriteRepository.SaveAsync();
            return Task.FromResult(new DeleteOrderCommandResponse()
            {
                Succeeded = true,
                Message = "Order deleted successfully."
            });
        }
    }
}
