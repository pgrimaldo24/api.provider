using MediatR;

namespace Ripley.Api.Provider.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<CreateProductCommandResponse>
    {
        public string DescriptionProduct { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public int SucursalId { get; set; }
        public decimal? BrutoVent { get; set; }
        public decimal ImpuestoVent { get; set; }
        public int MerchantId { get; set; }
    }
}
