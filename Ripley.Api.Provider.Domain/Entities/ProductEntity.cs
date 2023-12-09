namespace Ripley.Api.Provider.Domain.Entities
{
    public partial class ProductEntity
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public int SucursalId { get; set; }
        public decimal? BrutoVent { get; set; }
        public decimal? ImpuestoVent { get; set; }
        public int MerchantId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual CategoryEntity Category { get; set; } = null!;
        public virtual ProviderEntity Merchant { get; set; } = null!;
        public virtual SucursalEntity Sucursal { get; set; } = null!;
    }
}
