using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Customer.RemoveCustomer
{
    public class RemoveCustomerCommandResponse
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; }
    }
}
