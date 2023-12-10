using MediatR;

namespace Ripley.Api.Provider.Application.Products.Queries.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdateProductVm>
    {
        public int ProductId { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public int? Stock { get; set; }
        public int? SucursalId { get; set; }
        public decimal? BrutoVent { get; set; }
        public decimal? ImpuestoVent { get; set; }
        public int? MerchantId { get; set; }
    }
}
