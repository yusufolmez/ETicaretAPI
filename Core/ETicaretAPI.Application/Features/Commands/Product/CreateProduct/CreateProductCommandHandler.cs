using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly ILogger<CreateProductCommandHandler> _logger;
        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, ILogger<CreateProductCommandHandler> logger)
        {
            _productWriteRepository = productWriteRepository;
            _logger = logger;
        }
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            });
            await _productWriteRepository.SaveAsync();
            _logger.LogInformation($"Ürün oluşturuldu, İsim: {request.Name}");
            return new();
        }
    }
    
}
