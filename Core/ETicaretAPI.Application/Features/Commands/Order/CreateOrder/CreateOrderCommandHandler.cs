using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;

        public CreateOrderCommandHandler(IOrderHubService orderHubService, IOrderService orderService, IBasketService basketService)
        {
            _orderHubService = orderHubService;
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var basketItems = await _basketService.GetBasketItemsAsync();
            if (basketItems.Any())
                return new()
                {
                    Message = "Sepetinizde urun yok.",
                    Succeeded = false
                };
            else
            {
                await _orderService.CreateOrderAsync(new()
                {
                    Description = request.Description,
                    Address = request.Address,
                    BasketId = _basketService.GetUserActiveBasketAsync?.Id.ToString()
                });
                await _orderHubService.OrderAddedMessageAsync("Yeni bir siparis olusturuldu!");
                return new()
                {
                    Message = "Siparisiniz olusturulmustur.",
                    Succeeded = true
                };
            }
        }
    }
}
