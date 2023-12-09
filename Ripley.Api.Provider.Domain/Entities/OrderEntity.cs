namespace Ripley.Api.Provider.Domain.Entities
{
    public partial class OrderEntity
    {
        public string OrderGroup { get; set; } = null!;
        public DateTime FecTrx { get; set; }
        public int Sucursal { get; set; }
        public string? Cajero { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
