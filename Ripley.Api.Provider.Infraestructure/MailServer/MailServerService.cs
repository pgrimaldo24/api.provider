using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Ripley.Api.Provider.Application.Contracts.Infraestructure;
using Ripley.Api.Provider.Application.Mail.Model;
using Ripley.Api.Provider.CrossCutting.Configuration;
using System.Net;

namespace Ripley.Api.Provider.Infraestructure.MailServer
{
    public class MailServerService : IMailServerService
    {
        private readonly AppSetting _appSetting;

        public MailServerService(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public void SendEmail(ParametersMailSendingModel parametersMailSendingModel, CancellationToken ct = default)
        {
            try
            {
                if (ReferenceEquals(parametersMailSendingModel, null))
                    throw new Exception($"The parametersMailSendingModel object cannot be null");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var mineMessage = new MimeMessage();
                mineMessage.From.Add(new MailboxAddress("Ripley", parametersMailSendingModel.sender_email));
                mineMessage.Sender = new MailboxAddress("Ripley", parametersMailSendingModel.sender_email);

                var toaddresses = parametersMailSendingModel.receptor_email.Split(';');
                var ccaddresses = parametersMailSendingModel.cc.Split(';');

                if (toaddresses.Any())
                {
                    toaddresses.ToList().ForEach(x =>
                    {
                        mineMessage.To.Add(MailboxAddress.Parse(x.Trim()));
                    });
                }

                if (ccaddresses.Any())
                {
                    ccaddresses.ToList().ForEach(y =>
                    {
                        mineMessage.Cc.Add(MailboxAddress.Parse(y.Trim()));
                    });
                }

                var body = new BodyBuilder();
                mineMessage.Subject = parametersMailSendingModel.asunto;
                var body_message = parametersMailSendingModel.description.Split("\\n");

                body_message.ToList().ForEach(message =>
                {
                    body.HtmlBody += message + "<br/>";
                });

                mineMessage.Body = body.ToMessageBody();

                using (var smtp = new SmtpClient())
                {
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    smtp.CheckCertificateRevocation = true;
                    smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls, ct);
                    smtp.Authenticate(parametersMailSendingModel.sender_email, _appSetting.Password);
                    smtp.Send(mineMessage, ct);
                    smtp.Disconnect(true, ct);
                }
            }
            catch (Exception ex)
            {
                var message = string.Empty;
                if (ReferenceEquals(ex.InnerException, null))
                    message = ex.Message;
                else
                    message = ex.InnerException.Message;
                throw new Exception($"Oops! Error SendEmailKit {message}");
            }
        }
    }
}
