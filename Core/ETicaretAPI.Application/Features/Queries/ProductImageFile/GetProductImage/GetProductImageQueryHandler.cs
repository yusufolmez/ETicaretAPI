using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P= ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImage
{
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQueryRequest, List<GetProductImageQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration configuration;
        readonly ILogger<GetProductImageQueryHandler> _logger;

        public GetProductImageQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration, ILogger<GetProductImageQueryHandler> logger)
        {
            _productReadRepository = productReadRepository;
            this.configuration = configuration;
            _logger = logger;
        }

        public async Task<List<GetProductImageQueryResponse>>Handle(GetProductImageQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            _logger.LogInformation($"Ürün resimleri alındı, Ürün ID: {request.Id}");
            return product?.ProductImageFiles.Select( p => new GetProductImageQueryResponse
            {
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                FileName = p.FileName,
                Id = p.Id
            }).ToList();
        }
    }
}
