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

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }
        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UploadProductImageCommandResponse();
            
            try
            {
                P.Product product = await _productReadRepository.GetByIdAsync(request.ProductId);
                if (product == null)
                {
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

                response.Succeeded = true;
                response.Message = "Files uploaded successfully.";
                response.UploadedFileNames = result.Select(r => r.fileName).ToList();
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = "An error occurred while uploading files.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}
