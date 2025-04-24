using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Customer.RemoveCustomer
{
    public class RemoveCustomerCommandRequest : IRequest<RemoveCustomerCommandResponse>
    {
        public string Id { get; set; }
    }
}
