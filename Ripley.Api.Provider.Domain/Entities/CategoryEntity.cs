namespace Ripley.Api.Provider.Domain.Entities
{
    public partial class CategoryEntity
    {
        public CategoryEntity()
        {
            Products = new HashSet<ProductEntity>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
