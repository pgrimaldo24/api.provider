using MediatR;

namespace Ripley.Api.Provider.Application.Provider.Commands.CreateProvider
{
    public class CreateProviderCommand : IRequest<CreateProviderCommandResponse>
    {
        public string VendorNumber { get; set; }
        public string VendorName { get; set; }
        public string Address { get; set; }
        public string Rubro { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string Observations { get; set; }
    }
}
