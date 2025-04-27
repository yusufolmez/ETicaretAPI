using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.DTOs;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {

    }
    public class LoginUSerSuccessCommandResponse : LoginUserCommandResponse
    {
        public TokenDTO AccessToken { get; set; }
    }
    public class LoginUserFailedCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
