using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.GetAllCustomer
{
    public class GetAllCustomerQueryResponse
    {
        public int TotalCount { get; set; }
        public object Customers { get; set; }

    }
}
