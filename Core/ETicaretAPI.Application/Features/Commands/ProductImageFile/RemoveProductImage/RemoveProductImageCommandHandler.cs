using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using P = ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly ILogger<RemoveProductImageCommandHandler> _logger;
        public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ILogger<RemoveProductImageCommandHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _logger = logger;
        }
        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            P.ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));

            if (productImageFile != null)
            {
                product?.ProductImageFiles.Remove(productImageFile);
                await _productWriteRepository.SaveAsync();
                _logger.LogInformation($"Ürün resmi silindi, Ürün ID: {request.Id}, Resim ID: {request.ImageId}");
                return new() { Succeeded = true, Message = "Image deleted" };
            }
            else 
            {
                _logger.LogError($"Ürün resmi bulunamadı, Ürün ID: {request.Id}, Resim ID: {request.ImageId}");
                return new() { Succeeded = false, Message = "Image not found" };
            }
        }
    }
}
