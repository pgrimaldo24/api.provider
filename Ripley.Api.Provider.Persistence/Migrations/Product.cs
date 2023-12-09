using System;
using System.Collections.Generic;

namespace Ripley.Api.Provider.Persistence.Models
{
    public partial class Product
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

        public virtual Category Category { get; set; } = null!;
        public virtual Provider Merchant { get; set; } = null!;
        public virtual Sucursal Sucursal { get; set; } = null!;
    }
}
