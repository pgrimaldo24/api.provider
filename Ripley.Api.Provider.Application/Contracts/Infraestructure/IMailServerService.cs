using Ripley.Api.Provider.Application.Mail.Model;

namespace Ripley.Api.Provider.Application.Contracts.Infraestructure
{
    public interface IMailServerService
    {
        void SendEmail(ParametersMailSendingModel parametersMailSendingModel, CancellationToken ct = default);
    }
}
