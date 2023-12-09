namespace Ripley.Api.Provider.Application.Mail.Model
{
    public class ParametersMailSendingModel
    {
        public string? sender_email { get; set; }
        public string? receptor_email { get; set; }
        public string? asunto { get; set; }
        public string? description { get; set; }
        public string? cc { get; set; }
    }
}
