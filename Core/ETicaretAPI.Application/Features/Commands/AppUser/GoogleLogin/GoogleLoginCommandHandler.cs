using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IAuthService _authService;
        readonly ILogger<GoogleLoginCommandHandler> _logger;

        public GoogleLoginCommandHandler(IAuthService authService, ILogger<GoogleLoginCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.GoogleLoginAsync(request.IdToken, 15);
            _logger.LogInformation($"Google ile giriş başarılı, Token oluşturuldu");
            return new()
            {
                Token = token
            };
        }
    } 
}
