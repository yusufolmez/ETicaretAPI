using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using P = ETicaretAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly ILogger<UploadProductImageCommandHandler> _logger;
        public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, ILogger<UploadProductImageCommandHandler> logger)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _logger = logger;
        }
        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UploadProductImageCommandResponse();
            
            try
            {
                P.Product product = await _productReadRepository.GetByIdAsync(request.ProductId);
                if (product == null)
                {
                    _logger.LogError($"Ürün bulunamadı, ID: {request.ProductId}");
                    response.Succeeded = false;
                    response.Message = "Product not found.";
                    return response;
                }
                
                List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("productimages", request.Files);

                await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new P.ProductImageFile
                {
                    FileName = r.fileName,
                    Path = r.pathOrContainerName,
                    Storage = _storageService.StorageName,
                    Products = new List<P.Product>() { product }
                }).ToList());

                await _productImageFileWriteRepository.SaveAsync();

                _logger.LogInformation($"Ürün resimleri yüklendi, Ürün ID: {request.ProductId}, Dosya Sayısı: {result.Count}");
                response.Succeeded = true;
                response.Message = "Files uploaded successfully.";
                response.UploadedFileNames = result.Select(r => r.fileName).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Dosya yükleme hatası, Ürün ID: {request.ProductId}, Hata: {ex.Message}");
                response.Succeeded = false;
                response.Message = "An error occurred while uploading files.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}
