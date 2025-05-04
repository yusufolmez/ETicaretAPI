using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using P = ETicaretAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly ILogger<RemoveProductCommandHandler> _logger;

        public RemoveProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ILogger<RemoveProductCommandHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _logger = logger;
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            P.Product product = await _productReadRepository.GetByIdAsync(request.Id);
            if (product != null)
            {
                _productWriteRepository.Remove(product);
                await _productWriteRepository.SaveAsync();
                _logger.LogInformation($"Ürün silindi, ID: {request.Id}");
                return new() { Succeeded = true, Message = "Product deleted" };
            }
            else
            {
                _logger.LogError($"Ürün bulunamadı, ID: {request.Id}");
                return new() { Succeeded = false, Message = "Product not found" };
            }
        }
    }
}
