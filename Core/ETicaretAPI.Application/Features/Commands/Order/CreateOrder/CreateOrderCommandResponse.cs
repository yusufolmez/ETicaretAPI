﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandResponse
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; }
    }
}
