using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                UserName = request.Username,
                Email = request.Email,
                NameSurname = request.NameSurname
            }, request.Password);

            if (result.Succeeded)
            {
                return new() {Succeeded = true, Message = "User created successfully"};
            }
            else
            {
                return new(){Succeeded = false, Message = string.Join(", ", result.Errors.Select(x => x.Description))};
            }
        }
    }
}
