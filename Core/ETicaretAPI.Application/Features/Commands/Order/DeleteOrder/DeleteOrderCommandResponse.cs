using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Order.DeleteOrder
{
    public class DeleteOrderCommandResponse
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; }
    }
}
