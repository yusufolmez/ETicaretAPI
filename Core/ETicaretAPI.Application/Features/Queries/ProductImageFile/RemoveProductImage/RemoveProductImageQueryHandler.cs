using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageQueryHandler : IRequestHandler<RemoveProductImageQueryRequest, RemoveProductImageQueryResponse>
    {
        public async Task<RemoveProductImageQueryResponse> Handle(RemoveProductImageQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
