using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.RequestParameters;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Customer.GetAllCustomer
{
    public class GetAllCustomerQueryRequest : IRequest<GetAllCustomerQueryResponse>
    {
        public Pagination Pagination { get; set; }

    }
}
