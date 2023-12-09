namespace Ripley.Api.Provider.Domain.Entities
{
    public partial class SubOrderEntity
    {
        public string OrderGroup { get; set; } = null!;
        public string? SubOrderId { get; set; }
        public int ProductId { get; set; }
        public decimal? CouponDiscount { get; set; }
        public string? CouponCode { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? Commission { get; set; }
        public decimal? Total { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual OrderEntity OrderGroupNavigation { get; set; } = null!;
        public virtual ProductEntity Product { get; set; } = null!;
    }
}
