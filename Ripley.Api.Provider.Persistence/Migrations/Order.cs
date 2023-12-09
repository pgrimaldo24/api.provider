using System;
using System.Collections.Generic;

namespace Ripley.Api.Provider.Persistence.Models
{
    public partial class Order
    {
        public string OrderGroup { get; set; } = null!;
        public DateTime FecTrx { get; set; }
        public int Sucursal { get; set; }
        public string? Cajero { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
