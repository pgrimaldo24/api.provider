namespace Ripley.Api.Provider.Persistence.Models
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? DesSucursual { get; set; }
        public bool Active { get; set; }
        public string? CreatedBy { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
