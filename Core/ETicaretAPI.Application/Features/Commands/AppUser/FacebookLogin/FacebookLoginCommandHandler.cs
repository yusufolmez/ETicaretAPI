using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Logging;
namespace ETicaretAPI.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        readonly IAuthService _authService;
        readonly ILogger<FacebookLoginCommandHandler> _logger;

        public FacebookLoginCommandHandler(IAuthService authService, ILogger<FacebookLoginCommandHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.FacebookLoginAsync(request.AuthToken, 15);
            _logger.LogInformation($"Facebook ile giriş başarılı, Token oluşturuldu");
            return new()
            {
                Token = token,
            };
        }
    }
}
