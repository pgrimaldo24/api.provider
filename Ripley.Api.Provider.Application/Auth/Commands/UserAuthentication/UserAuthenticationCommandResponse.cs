using Ripley.Api.Provider.CrossCutting.Base;

namespace Ripley.Api.Provider.Application.Auth.Commands.UserAuthentication
{
    public class UserAuthenticationCommandResponse : BaseResponse
    {
        public UserAuthenticationCommandResponse() : base()
        {
        }

        public int ProviderId { get; set; }
        public string Token { get; set; }
    }
}
