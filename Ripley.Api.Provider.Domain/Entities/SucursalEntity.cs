namespace Ripley.Api.Provider.Domain.Entities
{
    public partial class SucursalEntity
    {
        public SucursalEntity()
        {
            Products = new HashSet<ProductEntity>();
        }

        public int Id { get; set; }
        public string? DES_SUCURSAL { get; set; }
        public bool ACTIVE { get; set; }
        public string? CreatedBy { get; set; }

        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
