using MediatR;

namespace Ripley.Api.Provider.Application.Auth.Commands.UserAuthentication
{
    public class UserAuthenticationCommand : IRequest<UserAuthenticationCommandResponse>
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
