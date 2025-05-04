using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        readonly IAuthService _authService;
        readonly ILogger<RefreshTokenLoginCommandHandler> _logger;

        public RefreshTokenLoginCommandHandler(IAuthService authService, ILogger<RefreshTokenLoginCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            TokenDTO token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            _logger.LogInformation($"Refresh token ile giriş başarılı, Token oluşturuldu");
            return new()
            {
                Token = token
            };
        }
    }
}
