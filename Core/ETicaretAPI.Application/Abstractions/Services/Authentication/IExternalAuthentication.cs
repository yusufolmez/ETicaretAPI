﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services.Authentication
{
    public interface IExternalAuthentication
    {
        Task<DTOs.TokenDTO> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
        Task<DTOs.TokenDTO> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
    }
}
