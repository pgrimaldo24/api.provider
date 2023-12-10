namespace Ripley.Api.Provider.Persistence.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }
        public bool Active { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
