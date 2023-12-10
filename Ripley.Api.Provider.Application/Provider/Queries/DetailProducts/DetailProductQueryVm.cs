using Ripley.Api.Provider.CrossCutting.Base;

namespace Ripley.Api.Provider.Application.Provider.Queries.DetailProducts
{
    public class DetailProductQueryVm
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; }
        public string Sucursal { get; set; }
        public decimal? BrutoVent { get; set; }
        public decimal? ImpuestoVent { get; set; } 
    }
}
